import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { AuthStorage } from './auth-storage';


interface LoginRequest {
  email: string;
  password: string;
}

interface LoginResponse {
  token: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly API_URL = 'http://localhost:5106';

  constructor(
    private http: HttpClient,
    private storage: AuthStorage
  ) {}

  login(data: LoginRequest) {
    return this.http
      .post<LoginResponse>(`${this.API_URL}/users/login`, data)
      .pipe(
        tap(response => {
          this.storage.setToken(response.token);
        })
      );
  }

  logout(): void {
    this.storage.clear();
  }

  isLoggedIn(): boolean {
    return this.storage.isAuthenticated();
  }
}
