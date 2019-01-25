import {Injectable} from '@angular/core';
import {ReplaySubject} from 'rxjs';
import {User} from '../models/user.interface';

@Injectable({
  providedIn: 'root'
})
export class DataSavingService {
  private readonly isLoggedInSource = new ReplaySubject<boolean>(1);
  private readonly authUserSource = new ReplaySubject<User>(1);
  isLoggedIn$ = this.isLoggedInSource.asObservable();
  authUser$ = this.authUserSource.asObservable();

  setIsLogged(isLogged: boolean) {
    this.isLoggedInSource.next(isLogged);
  }

  setAuthUser(authUser: User) {
    this.authUserSource.next(authUser);
  }

  logout () {
    this.setIsLogged(false);
    this.setAuthUser(null);
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  }
}
