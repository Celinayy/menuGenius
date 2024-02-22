import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category } from '../models/category.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private url = "http://localhost:8000/api/category"

  constructor(private connection: HttpClient) { }

  public getAllCategory(){
    return this.connection.get<Category[]>(this.url)
  }
}
