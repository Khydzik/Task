import {CanActivate, Router} from '@angular/router';
import {Injectable} from '@angular/core';

@Injectable()
export class HideLoginGuardService implements CanActivate {
  constructor (private router: Router) {}

  canActivate (): boolean {
    if (localStorage.getItem('token')) {
      this.router.navigate(['/posts']);

      return false;
    }

    return true;
  }
}
