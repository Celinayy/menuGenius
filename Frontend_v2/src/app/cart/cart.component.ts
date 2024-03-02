import { Component } from '@angular/core';
import { CartService } from '../services/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})

export class CartComponent {
  public deskId = "";

  constructor(public cartService: CartService) {}

  public get totalPrice() {
    return this.cartService.products.reduce((prev, product) => prev + product.price, 0);
  }

  public removeItem(index: number) {
    this.cartService.removeItem(index);
  }

  public checkout() {
    this.cartService.checkout(+this.deskId).subscribe((result) => {
      this.cartService.clear()
      window.location.replace(result.url)
    })
  }
}
