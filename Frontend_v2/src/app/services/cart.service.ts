import { Injectable } from '@angular/core';
import { Product } from '../models/product.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  public products: Product[] = [];

  constructor(private connection: HttpClient) {
    const previousState = sessionStorage.getItem("cart");
    if(previousState) {
      this.products = JSON.parse(previousState);
    }
  }

  private persist() {
    sessionStorage.setItem("cart", JSON.stringify(this.products));
  }

  public addProduct(product: Product) {
    this.products.push(product);
    this.persist();
  }

  public removeItem(index: number) {
    this.products.splice(index, 1);
    this.persist();
  }

  public checkout(deskId: number) {
    const token = localStorage.getItem("token");

    return this.connection.post<{url: string}>("http://localhost:8000/api/checkout",
      {
        products: this.products.map((product) => product.id),
        desk_id: deskId
      }, {
        headers: token ? {
          "Authorization": `Bearer ${token}`,
        } : {

        },
      }
    )
  }
  
  public clear() {
    this.products = []
    sessionStorage.removeItem("cart")
  }
}
