import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../models/product.model';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private url = "http://localhost:8000/api/product"
  public search = new BehaviorSubject<string>("");

  constructor(private connection: HttpClient) { }

  public listProducts() {
    return this.connection.get<Product[]>(this.url);
  }
  
  public listFoodProducts() {
    return this.listProducts().pipe(
      map(products => products.filter(product => product.is_food == true))
    );
  }

  public listDrinkProducts() {
    return this.listProducts().pipe(
      map(products => products.filter(product => product.is_food == false))
    );
  }
}