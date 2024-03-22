import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CategoryModel } from '../models/category-model';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {

  private url:string = `${environment.apiUrl}/api/category`



  constructor(private connection: HttpClient) { }

  public getAllCategory() {
    return this.connection.get<CategoryModel[]>(this.url)
  }
}
