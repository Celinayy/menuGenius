import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../models/product.model';
import { BehaviorSubject, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private url = "http://localhost:8000/api/product"
  public search = new BehaviorSubject<string>("");
  public foodProducts: Product[] = [];
  public drinkProducts: Product[] = [];

  constructor(private connection: HttpClient) {}

  public listProducts() {
    return this.connection.get<Product[]>(this.url);
  }
  
  public listFoodProducts() {
    return this.listProducts().pipe(
      map(products => products.filter(product => product.is_food == true))
      //map(products => {
        //this.foodProducts = products.filter(product => product.is_food == true);
        //return this.foodProducts;
      //})
    );
  }
  
  public listDrinkProducts() {
    return this.listProducts().pipe(
      map(products => products.filter(product => product.is_food == false))
    );
  }

  // public loadProductById(id: number) {
  //   return this.connection.get<Product>(`${this.url}/${id}`)
  // }
}
