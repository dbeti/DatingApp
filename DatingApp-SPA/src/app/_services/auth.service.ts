import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseURL = 'http://localhost:5000/api/auth/';
  jwtHelper = new JwtHelperService();

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(this.baseURL + 'login', model).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
        }
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
  }

  register(model: any) {
    return this.http.post(this.baseURL + 'register', model);
  }

  loggedIn() {
    const token = this.getToken();
    const isTokenExpired = this.jwtHelper.isTokenExpired(token);

    return !isTokenExpired;
  }

  getToken() {
    const token = localStorage.getItem('token');

    return token;
  }

  getDecodedToken() {
    const token = this.getToken();
    const decodedToken = this.jwtHelper.decodeToken(token);

    return decodedToken;
  }

  getUserName() {
    const token = this.getDecodedToken();

    if (token == null) {
      return null;
    }

    const userName = token.unique_name;

    return userName;
  }

}
