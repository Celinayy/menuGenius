import { Injectable } from '@angular/core';
import { ProductModel } from '../models/product-model';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  public products: ProductModel[] = [];

  constructor() {
    const previousState = sessionStorage.getItem("cart");
    if(previousState) {
      this.products = JSON.parse(previousState);
    }
  }

  private persist() {
    sessionStorage.setItem("cart", JSON.stringify(this.products));
  }

  public addProduct(product: ProductModel) {
    this.products.push(product);
    this.persist();
  }

  public removeItem(index: number) {
    this.products.splice(index, 1);
    this.persist();
  }
}
