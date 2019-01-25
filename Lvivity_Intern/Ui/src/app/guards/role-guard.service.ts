import {CanActivate, Router} from '@angular/router';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {map, tap} from 'rxjs/operators';
import {User} from '../models/user.interface';
import {DataSavingService} from '../services/data-saving.service';

@Injectable()
export class RoleGuardService implements CanActivate {
  constructor(
    private router: Router,
    private dataSavingService: DataSavingService
  ) {}

  canActivate(): Observable<boolean> | boolean {
    const authUser: User = JSON.parse(localStorage.getItem('user'));

    if (authUser && authUser.role.name === 'admin') {
      return true;
    }

    return this.dataSavingService.authUser$
      .pipe(
        map(user => user && user.role.name === 'admin'),
        tap((isAdmin: boolean) => {
          if (isAdmin) {
            return true;
          }

          this.router.navigate(['/posts']);

          return false;
        })
      );
  }
}
