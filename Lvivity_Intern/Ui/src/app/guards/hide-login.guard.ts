import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';
import { Observable} from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable()
export class HideLoginGuard implements CanActivate {
  constructor (
    private authService: AuthService,
    private router: Router
  ) {}

  canActivate (
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | Promise<boolean> | boolean {
    const isAuth = this.authService.getIsAuth();

    if (isAuth) {
      this.router.navigate(['/posts']);
    }

    return !isAuth;
  }
}
