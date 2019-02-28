import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { User } from '../../models/user.interface';
import { Role } from '../../models/role.interface';
import { PageEvent } from '@angular/material';
import { Subscription } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  users: User[] = [];
  totalUsers = 0;
  perPage = 2;
  currentPage = 1;
  pageSizeOptions = [1, 2, 5, 10];
  private usersSub: Subscription;

  constructor(
    private authService: AuthService,
    private spinner: NgxSpinnerService
  ) {}

  ngOnInit() {
    this.getUsers();
  }

  getUsers() {
    this.spinner.show();
    this.authService.getUsers(this.perPage, this.currentPage);
    this.usersSub = this.authService.getUsersUpdateListener()
      .subscribe(
        (users: User[]) => {
          console.log(users);
          this.spinner.hide();
          this.users = users;
          this.totalUsers = this.users.length;
        }
      );
  }

  changeUserRole (id: number, role: number) {
    this.spinner.show();
    const roleId = role === 1 ? 2 : 1; 
    this.authService.changeUserRole(id, roleId);
    this.authService.getUsers(this.perPage, this.currentPage);
  }

  onChangedPage (pageData: PageEvent) {
    this.spinner.show();
    this.currentPage = pageData.pageIndex + 1;
    this.perPage = pageData.pageSize;
    this.authService.getUsers(this.perPage, this.currentPage);
  }
}
