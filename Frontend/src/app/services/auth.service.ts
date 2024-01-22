import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserModel } from '../models/user-model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private url = "http://localhost:8000/api";

  public user?: UserModel;

  constructor(private connection: HttpClient) {
    this.loadUser()
   }

  public login(email: string, password: string) {
    return this.connection.post<{ token: string }>(`${this.url}/login`, {
      email,
      password,
    });
  }

  public register(name: string, email: string, phone: string, password: string, passwordAgain: string) {
    return this.connection.post<{ token: string }>(`${this.url}/register`, {
      name,
      email,
      phone,
      password,
      password_confirmation: passwordAgain,
    });
  }

  private loadUser() {
    this.getUser().subscribe((result) => {
      this.user = result;
    })
  }

  public getUser() {
    return this.connection.get<UserModel>("http://localhost:8000/api/user",
    {headers: {Authorization: `Bearer ${localStorage.getItem("token")}`}})
  }
}
