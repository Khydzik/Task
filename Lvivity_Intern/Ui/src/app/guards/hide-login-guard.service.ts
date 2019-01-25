import {CanActivate, Router} from '@angular/router';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {tap} from 'rxjs/operators';
import {DataSavingService} from '../services/data-saving.service';

@Injectable()
export class HideLoginGuardService implements CanActivate {
  constructor(
    private router: Router,
    private dataSavingService: DataSavingService
  ) {}

  canActivate(): Observable<boolean> | boolean {
    if (!localStorage.getItem('token')) {
      return true;
    }

    return this.dataSavingService.isLoggedIn$
      .pipe(
        tap(isLoggedIn => {
          if (!isLoggedIn) {
            return true;
          }
          this.router.navigate(['/posts']);
          return true;
        })
      );
  }
}
