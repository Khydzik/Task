import {Component, ViewEncapsulation} from '@angular/core';
import { ApiService } from '../../services/api.service';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import {User} from '../../models/user.interface';
import {DataSavingService} from '../../services/data-saving.service';
import { Role } from 'src/app/models/role.interface';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['../../styles/shared-form.style.css'],
  encapsulation: ViewEncapsulation.None
})
export class LoginComponent {
  errorMessages = {
    userName: null,
    password: null,
    notFound: null
  };

  constructor(
    private apiService: ApiService,
    private dataSavingService: DataSavingService,
    private router: Router
  ) {}

  login(form: NgForm): void {
    if (form.valid) {
      this.apiService.doLogin(form.value)
        .then(
          (res: {
            access_token: string,
            id: string,
            username: string,
            role: Role
          }) => {
            const authUser: User = {
              id: res.id,
              userName: res.username,
              role: res.role            
            };           

            localStorage.setItem('token', res.access_token);
            localStorage.setItem('user', JSON.stringify(authUser));
            this.dataSavingService.setIsLogged(true);
            this.dataSavingService.setAuthUser(authUser);
            this.router.navigate(['/posts']);
          }
        )
        .catch(err => {
          if (typeof err.error === 'string') {
            this.errorMessages.notFound = err.error;
          } else {
            if (err.error.UserName) {
              this.errorMessages.userName = err.error.UserName[0];
            }

            if (err.error.Password) {
              this.errorMessages.password = err.error.Password[0];
            }
          }
        });
    }
  }
}
