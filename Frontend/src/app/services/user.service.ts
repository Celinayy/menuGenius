import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { UserModel } from '../models/user-model';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private url:string = `${environment.apiUrl}/api/user`

  constructor(private connection: HttpClient) { }

  public get() {
    return this.connection.get<UserModel>(this.url,
    {headers: {Authorization: `Bearer ${localStorage.getItem("token")}`}})
  }
}
