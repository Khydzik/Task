import {Component, OnDestroy, OnInit} from '@angular/core';
import {User} from './models/user.interface';
import {DataSavingService} from './services/data-saving.service';
import {Subscription} from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  isLoggedIn = false;
  isLoggedSub: Subscription;

  constructor(private dataSavingService: DataSavingService) {}

  ngOnInit(): void {
    const user: User = JSON.parse(localStorage.getItem('user'));

    if (localStorage.getItem('token')) {
      this.dataSavingService.setIsLogged(true);
    } else {
      this.dataSavingService.setIsLogged(false);
    }

    if (user) {
      this.dataSavingService.setAuthUser(user);
    } else {
      this.dataSavingService.setAuthUser(null);
    }

    this.isLoggedSub = this.dataSavingService.isLoggedIn$
      .subscribe(res => {
        this.isLoggedIn = res;
      });
  }

  ngOnDestroy(): void {
    this.isLoggedSub.unsubscribe();
  }
}
