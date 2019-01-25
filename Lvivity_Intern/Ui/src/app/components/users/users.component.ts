import { Component, OnInit } from '@angular/core';
import {ApiService} from '../../services/api.service';
import {User} from '../../models/user.interface';
import {ShowToastrService} from '../../services/show-toastr.service';
import {Role} from '../../models/role.interface';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  isLoading = false;
  users: User[] = [];

  constructor(
    private apiService: ApiService,
    private toastr: ShowToastrService,
  ) {}

  ngOnInit() {
    this.isLoading = true;
    this.apiService.getUsers()
      .subscribe((users: User[]) => {
        this.isLoading = false;
        this.users = users;
      });
  }

  changeUserRole (id: string, role: Role) {
    this.isLoading = true;
    this.apiService.changeUserRole(id, role)
      .then((newRole: Role) => {
        this.isLoading = false;
        const newUsers = [...this.users];
        const oldUserIndex: number = newUsers.findIndex(user => user.id === id);
        const oldUser: User = newUsers[oldUserIndex];
        newUsers[oldUserIndex] = {
          ...oldUser,
          role: newRole
        };
        this.users = newUsers;
        this.toastr.showSuccess('Роль змінено!');
      })
      .catch(err => {
        this.toastr.showError('Виникли проблеми з зміною ролі!', err);
      });
  }
}
