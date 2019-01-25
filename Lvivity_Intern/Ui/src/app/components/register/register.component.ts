import {Component, ViewEncapsulation} from '@angular/core';
import { NgForm } from '@angular/forms';
import { ApiService } from '../../services/api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['../../styles/shared-form.style.css'],
  encapsulation: ViewEncapsulation.None
})
export class RegisterComponent {
  errorMessages = {
    userName: null,
    password: null,
    confirmPassword: null,
    userExist: null
  };

  constructor(
    private apiService: ApiService,
    private router: Router
  ) {}

  register (form: NgForm) {
    if (form.valid) {
      this.apiService.doRegister(form.value)
        .then(() => {
          this.router.navigate(['/login']);
        })
        .catch(err => {
          if (typeof err.error === 'string') {
            this.errorMessages.userExist = err.error;
          } else {
            if (err.error.UserName) {
              this.errorMessages.userName = err.error.UserName[0];
            }

            if (err.error.Password) {
              this.errorMessages.password = err.error.Password[0];
            }

            if (err.error.ConfirmPassword) {
              this.errorMessages.confirmPassword = err.error.ConfirmPassword[0];
            }
          }
        });
    }
  }
}
