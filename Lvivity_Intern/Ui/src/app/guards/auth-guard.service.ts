import {CanActivate, Router} from '@angular/router';
import {Injectable} from '@angular/core';

@Injectable()
export class AuthGuardService implements CanActivate {
  constructor (private router: Router) {}

  canActivate(): boolean {
    if (localStorage.getItem('token')) {
      return true;
    }

    this.router.navigate(['/login']);
  }
}
