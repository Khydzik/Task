import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {HttpClientModule} from '@angular/common/http';
import {FormsModule} from '@angular/forms';
import {AppRoutingModule} from './app-routing.module';

import {AppComponent} from './app.component';
import {LoginComponent} from './components/login/login.component';
import {PostsComponent} from './components/posts/posts.component';
import {HeaderComponent} from './components/header/header.component';

import {ApiService} from './services/api.service';
import {HideLoginGuardService} from './guards/hide-login-guard.service';
import {AuthGuardService} from './guards/auth-guard.service';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    PostsComponent,
    HeaderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    ApiService,
    HideLoginGuardService,
    AuthGuardService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
