import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';
import { AuthService } from '_service/auth.service';
import { AlertifyService } from '_service/alertify.service';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { RouterModule } from '@angular/router';
import { appRoutes } from './router';
import { UserService } from '_service/user.service';
export function tokenGetterFunc() {
  return localStorage.getItem('token');
}
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule.forRoot(appRoutes),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetterFunc,
        whitelistedDomains: ['localhost:5000'],
        blacklistedRoutes: ['localhost:5000/api/auth']
      }
    }),

  ],
  providers: [
    AuthService,
    AlertifyService,
    UserService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
