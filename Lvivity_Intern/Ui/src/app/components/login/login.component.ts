import {Component} from '@angular/core';
import {ApiService} from '../../services/api.service';
import {AuthUser} from '../../models/auth-user.interface';
import {Router} from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  userData: AuthUser = {
    userName: '',
    password: ''
  };

  constructor(
    private apiService: ApiService,
    private router: Router
  ) {}

  doLogin(): void {
    if (this.userData.userName && this.userData.password) {
    this.apiService.doLogin(this.userData)
      .then(
        () => {
          this.router.navigate(['/posts']);
        },
        err => {
          console.log(err);
        }
      );
    }
  }
}
