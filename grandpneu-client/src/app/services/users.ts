import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from 'src/app/core/models/user.model';

@Injectable({
  providedIn: 'root'
})
export class Users {

  private readonly API_URL = 'http://localhost:5106';

  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get<User[]>(`${this.API_URL}/users`);
  }

  update(userId: string, dto: { name: string }) {
    return this.http.put(`${this.API_URL}/users/${userId}`, dto);
  }

  register(user: { name: string; email: string; role: number }) {
    return this.http.post(`${this.API_URL}/users/register`, user);
  }

}
