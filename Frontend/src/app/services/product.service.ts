import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ProductModel } from '../models/product-model';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private url = "http://localhost:8000/api/product"
  public search = new BehaviorSubject<string>("");

  constructor(private connection: HttpClient) { }

  public listProducts() {
    return this.connection.get<ProductModel[]>(this.url)
  }
}
