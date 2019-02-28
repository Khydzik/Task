import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit, OnDestroy {
  isAuthenticated = false;
  isAdmin = false;
  private authStatusSub: Subscription;

  constructor (private authService: AuthService) {}

  ngOnInit (): void {
    this.isAuthenticated = this.authService.getIsAuth();
    const authUser = this.authService.getAuthUser();

    if (authUser) {
      this.isAdmin = authUser.role === 'admin';
    }

    this.authStatusSub = this.authService.getAuthStatus()
      .subscribe((isAuth: boolean) => {
        this.isAuthenticated = isAuth;

        if (this.isAuthenticated) {
        this.isAdmin = this.authService.getAuthUser().role === 'admin';
        }
      });
  }

  onLogout () {
    this.authService.logout();
  }

  ngOnDestroy (): void {
    this.authStatusSub.unsubscribe();
  }
}
