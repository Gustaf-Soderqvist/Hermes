import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';

import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { AboutComponent } from './about/about.component';
import { GuestbookComponent } from './guestbook/guestbook.component';
import { GuestbookService } from "./service/guestbook.service";


import { MDBBootstrapModule } from 'angular-bootstrap-md';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AboutComponent,
    GuestbookComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    MDBBootstrapModule.forRoot(),
    ApiAuthorizationModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, data: { title: 'Home' } },
      { path: 'about', component: AboutComponent, pathMatch: 'full' },
      { path: 'guestbook', component: GuestbookComponent, pathMatch: 'full', canActivate: [AuthorizeGuard] },
    ])
  ],
  providers: [
    GuestbookService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthorizeInterceptor, multi: true
    },

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
