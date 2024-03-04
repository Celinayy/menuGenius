import { Injectable, OnInit } from '@angular/core';
import { Product } from '../models/product.model';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user.model';
import { AuthService } from './auth.service';
import { CartProduct } from '../models/cart-product.model';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  public cartProducts: CartProduct[] = [];
  public userId: number = -1;

  constructor(private connection: HttpClient, private authService: AuthService) {
    this.authService.getUser().subscribe((user) =>{
      this.userId = user.id;
    });
  }

  public loadCartData() {
    this.cartProducts = [];
    if (this.userId > -1) {
      const previousState = localStorage.getItem(`cart_${this.userId}`);
      if (previousState) {
        this.cartProducts = JSON.parse(previousState);
      }
    } else {
      const previousState = sessionStorage.getItem('cart');
      if (previousState){
        this.cartProducts = JSON.parse(previousState);
      }
    }
  }

  private persist() {
    const dataToPersist = this.cartProducts
  
    if (this.userId > -1) {
      localStorage.setItem(`cart_${this.userId}`, JSON.stringify(dataToPersist));
    } else {
      sessionStorage.setItem('cart', JSON.stringify(dataToPersist));
    }
  }
  
  public addProduct(product: Product) {
    const cartProduct: CartProduct = this.createCartItemFromProduct(product);
    this.cartProducts.push(cartProduct);
    this.persist();
  }

  private createCartItemFromProduct(product: Product): CartProduct {
    return {
        id: product.id,
        name: product.name,
        packing: product.packing,
        price: product.price,
    };
  }

  public removeItem(index: number) {
    this.cartProducts.splice(index, 1);
    this.persist();
  }

  public checkout(deskId: number) {
    const token = localStorage.getItem("token");

    return this.connection.post<{url: string}>("http://localhost:8000/api/checkout",
      {
        products: this.cartProducts.map((product) => product.id),
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
    this.cartProducts = []
    sessionStorage.removeItem("cart")
  }
}
