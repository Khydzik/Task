import {Component, OnDestroy, OnInit} from '@angular/core';
import {Subject, Subscription} from 'rxjs';
import {takeUntil} from 'rxjs/operators';
import {DataSavingService} from '../../services/data-saving.service';
import {User} from '../../models/user.interface';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit, OnDestroy {
  authUser: User;
  isAdmin = false;
  authUserSub: Subscription;

  constructor(private dataSavingService: DataSavingService) {}

  ngOnInit(): void {
    this.authUserSub = this.dataSavingService.authUser$
      .subscribe(res => {
        this.authUser = res;

        if (this.authUser) {
          this.isAdmin = this.authUser.role.name === 'admin';
        }
      });
  }

  onLogout () {
    this.dataSavingService.logout();
  }

  ngOnDestroy(): void {
    this.authUserSub.unsubscribe();
  }
}
