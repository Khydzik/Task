import { Component, ViewEncapsulation } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['../../styles/shared-form.style.css'],
  encapsulation: ViewEncapsulation.None
})
export class LoginComponent {
  constructor(
    private authService: AuthService
  ) {}

  login(form: NgForm): void {
    if (form.valid) {
      this.authService.doLogin(form.value);
    }
  }
}
