import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {AuthUser} from '../models/auth-user.interface';
import {Observable} from 'rxjs';

@Injectable()
export class ApiService {
  private readonly api = 'http://localhost:5000/api/Security';
  private readonly apiDashboard = 'http://localhost:5000/api/Dashboard';

  constructor(private http: HttpClient) {}

  doLogin(authUser: AuthUser): Promise<object> {
    return this.http.post(this.api,authUser).toPromise();
  }

  getPosts(): Observable<object> {
    return this.http.get(`${this.apiDashboard}`);
  }
}
