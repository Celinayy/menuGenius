import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private url = "http://localhost:8000/api";

  constructor(private connection: HttpClient) { }

  public login(email: string, password: string) {
    return this.connection.post<{ token: string }>(`${this.url}/login`, {
      email,
      password,
    });
  }

  public register(name: string, email: string, password: string, passwordAgain: string) {
    return this.connection.post<{ token: string }>(`${this.url}/register`, {
      name,
      email,
      password,
      password_confirmation: passwordAgain,
    });
  }
}
