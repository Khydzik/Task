import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LoginComponent } from './components/login/login.component';
import { PostsComponent } from './components/posts/posts.component';
import { RegisterComponent } from './components/register/register.component';
import { UsersComponent } from './components/users/users.component';
import { CreatePostComponent } from './components/createPost/createPost.component';

import { HideLoginGuard } from './guards/hide-login.guard';
import { AuthGuard } from './guards/auth.guard';
import { RoleGuard } from './guards/role.guard';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'login',
    component: LoginComponent,
    canActivate: [HideLoginGuard]
  },
  {
    path: 'createPost',
    component: CreatePostComponent,
   // canActivate: [HideLoginGuardService]
  },
  {
    path: 'register',
    component: RegisterComponent,
    canActivate: [HideLoginGuard]
  },
  {
    path: 'posts',
    component: PostsComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'users',
    component: UsersComponent,
    canActivate: [AuthGuard, RoleGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [
    HideLoginGuard,
    RoleGuard,
    AuthGuard
  ]
})
export class AppRoutingModule {}
