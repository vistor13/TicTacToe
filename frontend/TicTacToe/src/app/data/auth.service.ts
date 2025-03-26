import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {LoginResponse} from '../interfaces/auth.interface';
import {catchError, tap, throwError} from 'rxjs';
import {CookieService} from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private cookieService = inject(CookieService);
  http = inject(HttpClient);

  token: string | null = null;
  private apiUrl = 'http://localhost:5120/api/auth';

  get isAuth(){
    if (this.token == null){
      this.token = this.cookieService.get('token');
    }
    return !!this.token;
  }

  logout() {
    this.cookieService.delete('token');
    this.token = null;
  }

  register(payload: { email: string, password: string, firstName: string, lastName: string }) {
    return this.http.post(`${this.apiUrl}/register`, {
      email: payload.email,
      password: payload.password,
      firstName: payload.firstName,
      lastName: payload.lastName
    }).pipe(
      catchError(error => {
        console.error('Registration error:', error);
        return throwError(error);
      })
    );
  }


  login(payload: { email: string, password: string }) {
    return this.http.post<LoginResponse>(`${this.apiUrl}/login`, {
      login: payload.email,
      password: payload.password
    }).pipe(
      tap(res => {
        this.token = res.access_token;
        this.cookieService.set('token', res.access_token);
      })
    );
  }

}
