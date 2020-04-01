import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '_models/user';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl + 'auth/';
  currentUser: User;
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  constructor(private http: HttpClient) { }
  login(model: any) {
    return this.http.post(this.baseUrl + 'login', model).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
          localStorage.setItem('user', JSON.stringify(user.userFromRepo));
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          this.currentUser = user.userFromRepo;
        }
      })
    );
  }
  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }
}

