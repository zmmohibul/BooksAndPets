import { Injectable, signal, WritableSignal } from '@angular/core';
import { environment } from '../../environments/environment';
import { User } from '../models/authentication/user';
import { UserDetails } from '../models/authentication/userDetails';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { LoginModel } from '../models/authentication/loginModel';
import { map, tap } from 'rxjs';
import { RegisterModel } from '../models/authentication/registerModel';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  baseUrl = environment.apiUrl;
  user: WritableSignal<User | null> = signal(null);
  userDetails: WritableSignal<UserDetails | null> = signal(null);
  lastPage = '';
  constructor(
    private http: HttpClient,
    private router: Router,
  ) {}

  register(model: RegisterModel) {
    return this.http
      .post<User>(`${this.baseUrl}/authentication/register`, model)
      .pipe(
        tap((response) => {
          this.user.set(response);
          localStorage.setItem('user', JSON.stringify(response));
          this.loadUserDetails().subscribe();
        }),
      );
  }

  login(model: LoginModel) {
    return this.http
      .post<User>(`${this.baseUrl}/authentication/login`, model)
      .pipe(
        tap((response) => {
          this.user.set(response);
          localStorage.setItem('user', JSON.stringify(response));
          this.loadUserDetails().subscribe();
        }),
      );
  }

  loadUserDetails() {
    return this.http
      .get<UserDetails>(`${this.baseUrl}/authentication/user-detail`)
      .pipe(
        tap((response) => {
          this.userDetails.set(response);
          localStorage.setItem('userDetails', JSON.stringify(response));
        }),
      );
  }

  logout() {
    localStorage.removeItem('user');
    localStorage.removeItem('userDetails');

    this.user.set(null);
    this.userDetails.set(null);

    this.router.navigateByUrl('');
  }

  setUserIfLoggedIn() {
    let userStr = localStorage.getItem('user');
    let userDetailsStr = localStorage.getItem('userDetails');
    if (!userStr || !userDetailsStr) {
      return;
    }

    const user: User = JSON.parse(userStr);
    this.user.set(user);

    const userDetails: UserDetails = JSON.parse(userDetailsStr);
    this.userDetails.set(userDetails);
  }
}
