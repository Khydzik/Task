import { Injectable,ViewChild} from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Subject, throwError, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { RegData } from '../models/reg-data.interface';
import { LoginData } from '../models/login-data.interface';
import { User } from '../models/user.interface';
import { Role } from '../models/role.interface';
import { ShowToastrService } from './show-toastr.service';
import { map } from 'rxjs/operators';
import { CreatePost } from '../models/createPost.interface';

const API_URL = 'http://localhost:5000/api/';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private users: User[] = [];
  private isAuthenticated = false;
  private token: string;
  private authUser: User;
  private authStatusSource = new Subject<boolean>();
  private usersUpdated = new Subject<User[]>();

  constructor(
    private http: HttpClient,
    private router: Router,
    private toastr: ShowToastrService
  ) {}

  getToken () {
    return this.token;
  }

  getIsAuth () {
    return this.isAuthenticated;
  }

  getAuthUser () {
    return this.authUser;
  }

  getAuthStatus () {
    return this.authStatusSource.asObservable();
  }

  getUsersUpdateListener () {
    return this.usersUpdated.asObservable();
  }

  doCreatePost(formData: CreatePost): Promise<Object> {
    return this.http.post(`${API_URL + 'CreatePost'}`, formData).toPromise();
  }

  doRegister(formData: RegData): void {
    this.http.post(API_URL + 'Register', formData)
      .subscribe(
        () => {
          this.toastr.showSuccess('Registration successful, now you can log in');
          this.router.navigate(['/login']);
        },
        err => {
          this.toastr.showError(err.error.Error.Message, err);
          this.authStatusSource.next(false);
        }
      );
  }
  
  upload(formData:FormData){
    this.http.post<CreatePost>(API_URL + 'CreateNewPost', formData).subscribe(
      () => {
        this.toastr.showSuccess('Create successful, now you can see post');
        this.router.navigate(['/posts']);
      },
      err => {
        this.toastr.showError(err.error.Error.Message, err);
      }             
    );    
   }  

  doLogin(formData: LoginData) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Accept-Language': window.navigator.language
      })
    };

    this.http.post(API_URL + 'Security', formData, httpOptions)
      .subscribe(
        (res: {
          result: {
            id: number,
            username: string,
            access_token: string,
            role: { name: string }
          },
          error: { message: string }
        }) => {
          const token = res.result.access_token;
          this.token = token;

          if (token) {
            this.isAuthenticated = true;
            this.authUser = {
              id: res.result.id,
              userName: res.result.username,
              role: res.result.role
            };
            this.authStatusSource.next(true);
            this.saveAuthData(token, this.authUser);
            this.toastr.showSuccess('Successfully logged in');
            this.router.navigate(['/posts']);
          }
        },
        err => {
          this.toastr.showError(err.error.Error.Message, err);
          this.authStatusSource.next(false);
        }
      );
  }
  
  getUsers(perPage: number, currentPage: number): void {
    this.http.post<{
      result: {
        user: User[]
      },
      error: { message: string }
    }>(API_URL + 'User', { perPage, currentPage }) .pipe(map(res => {
      if (res.error) {
        throwError(res.error);
      } else {
        return res.result.user;
      }
    }))
      .subscribe(users => {
        this.users = users;
        this.usersUpdated.next([...this.users]);
      }, err => {
        this.toastr.showError(err.message, err);
        
      });
  }

  changeUserRole(id: number, role: Role): void {
    this.http.patch(API_URL + 'Edit', { id, role })
      .subscribe(
        (result: { name: Role }) => {
          const oldUsers = [...this.users];
          const oldUserIndex: number = oldUsers.findIndex(user => user.id === id);
          const oldUser: User = oldUsers[oldUserIndex];
          oldUsers[oldUserIndex] = {
            ...oldUser,
          role: result.name
          };
          this.users = oldUsers;
        }
      );
  }

  autoAuthUser() {
    const authInfo = this.getAuthData();

    if (!authInfo) {
      return;
    }

    this.token = authInfo.token;
    this.authUser = authInfo.user;
    this.isAuthenticated = true;
    this.authStatusSource.next(true);
  }

  logout() {
    this.token = null;
    this.isAuthenticated = false;
    this.authStatusSource.next(false);
    this.authUser = null;
    this.clearAuthData();
    this.router.navigate(['/']);
  }

  private saveAuthData(token: string, authUser: User) {
    localStorage.setItem('token', token);
    localStorage.setItem('user', JSON.stringify(authUser));
  }

  private clearAuthData() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  }

  private getAuthData() {
    const token = localStorage.getItem('token');
    const user = localStorage.getItem('user');

    if (!token || !user) {
      return;
    }

    return { token, user: JSON.parse(user) };
  }
}
