import { Component, ViewEncapsulation } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['../../styles/shared-form.style.css'],
  encapsulation: ViewEncapsulation.None
})
export class RegisterComponent {
  constructor(
    private authService: AuthService,
  ) {}

  register (form: NgForm) {
    if (form.valid) {
      this.authService.doRegister(form.value);
    }
  }
}
