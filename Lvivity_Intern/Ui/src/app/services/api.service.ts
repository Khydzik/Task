import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {RegData} from '../models/reg-data.interface';
import {LoginData} from '../models/login-data.interface';
import { Role } from '../models/role.interface';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private readonly api = 'http://www.localhost:5000/api/';

  constructor(private http: HttpClient) {}

  doLogin(formData: LoginData): Promise<Object> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Accept-Language': window.navigator.language
      })
    };

    return this.http.post(`${this.api + 'Security'}`, formData, httpOptions).toPromise();
  }

  doRegister(formData: RegData): Promise<Object> {
    return this.http.post(`${this.api + 'Register'}`, formData).toPromise();
  }

  getPosts(): Observable<Object> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.getToken()}`
      })
    };

    return this.http.get(`${this.api + 'Dashboard'}`, httpOptions);
  }

  getUsers(): Observable<Object> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.getToken()}`
      })
    };
    return this.http.get(`${this.api + 'User'}`, httpOptions);
  }

  changeUserRole(id: string, role: Role): Promise<Object> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.getToken()}`
      })
    };
    return this.http.patch(`${this.api + 'Edit'}`, { id, role }, httpOptions).toPromise();
  }

  getToken() {
    return localStorage.getItem('token');
  }
}
