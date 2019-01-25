import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';

import {AuthGuardService} from './guards/auth-guard.service';
import {HideLoginGuardService} from './guards/hide-login-guard.service';
import {RoleGuardService} from './guards/role-guard.service';

import {LoginComponent} from './components/login/login.component';
import {PostsComponent} from './components/posts/posts.component';
import {RegisterComponent} from './components/register/register.component';
import {UsersComponent} from './components/users/users.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'login',
    component: LoginComponent,
    canActivate: [HideLoginGuardService]
  },
  {
    path: 'register',
    component: RegisterComponent,
    canActivate: [HideLoginGuardService]
  },
  {
    path: 'posts',
    component: PostsComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: 'users',
    component: UsersComponent,
    canActivate: [AuthGuardService, RoleGuardService]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
