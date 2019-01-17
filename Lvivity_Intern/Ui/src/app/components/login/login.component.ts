import {Component, OnDestroy} from '@angular/core';
import {ApiService} from '../../services/api.service';
import {AuthUser} from '../../models/auth-user.interface';
import {Router} from '@angular/router';
import {Subscription} from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnDestroy {
  userData: AuthUser = {
    userName: '',
    password: ''
  };

  errorMessages = {
  userName: null,
  password: null,
  notFound: null
  };

  loginSub: Subscription;

  constructor(
    private apiService: ApiService,
    private router: Router
  ) {}

  ngOnInit():void{
    const lng  = window.navigator .language;
    console.log((lng));    
  }
  
  doLogin(): void {
    this.loginSub = this.apiService.doLogin(this.userData)
      .subscribe(
        (token: string) => {
          console.log(token);
          localStorage.setItem('token', JSON.stringify(token));
           this.router.navigate(['/posts']);
        },
        err => { 
          console.log(err);
          if(typeof err.error === 'string')
          {
            this.errorMessages.notFound =  err.error;
          }else{
            if(err.error.UserName)
            {
              this.errorMessages.userName = err.error.UserName[0];
            }
            if(err.error.Password)
            {
              this.errorMessages.password = err.error.Password[0];
            }
          }
        }
      );
  }

  ngOnDestroy() {
    this.loginSub.unsubscribe();
  }
}
