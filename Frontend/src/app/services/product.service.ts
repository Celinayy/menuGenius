import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { ProductModel } from '../models/product-model';
import { BehaviorSubject } from 'rxjs';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private url:string = `${environment.apiUrl}/api/product`


  public search = new BehaviorSubject<string>("");

  public products: ProductModel[] = [];

  constructor(private connection: HttpClient) {
    this.listProducts().subscribe((products) => {
      this.products = products;
    });
  }

  public listProducts() {
    return this.connection.get<ProductModel[]>(this.url)
  }

  public listDrinks() {
    return this.products.filter((product) => {
      return !product.is_food
    })
  }

  public listFoods() {
    return this.products.filter((product) => {
      return product.is_food
    })
  }


  public loadProductById(id: number) {
    return this.connection.get<ProductModel>(`${this.url}/${id}`)
  }

}
