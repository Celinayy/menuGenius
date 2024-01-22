import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserModel } from '../models/user-model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private connection: HttpClient) { }

  public get() {
    return this.connection.get<UserModel>("http://localhost:8000/api/user",
    {headers: {Authorization: `Bearer ${localStorage.getItem("token")}`}})
  }
}
