import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {AuthUser} from '../models/auth-user.interface';
import {Observable} from 'rxjs'; 
 
@Injectable()
export class ApiService {
  private readonly api = 'http://www.localhost:5000/api';

  constructor(private http: HttpClient) {}
 
 
  doLogin(authUser: AuthUser): Observable<Object> {
    const httpOptions = {
      headers:new HttpHeaders({
       'Accept-Language': window.navigator.language
      })
    }
    return this.http.post(`${this.api + '/Security'}`, authUser,httpOptions);
  }

  getPosts(): Observable<Object> {
    
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${this.getToken().access_token}`,
      })
    };

    return this.http.get(`${this.api + '/Dashboard'}`, httpOptions);
  }

  getToken() {
    return JSON.parse(localStorage.getItem('token'));
  }
}
