import { Component } from '@angular/core';
import { CartService } from '../services/cart.service';
import { ToastrService } from 'ngx-toastr';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-cart-window',
  templateUrl: './cart-window.component.html',
  styleUrls: ['./cart-window.component.css']
})



export class CartWindowComponent {

  public deskId = "";


  constructor(public cart: CartService, private toast: ToastrService, ) {}

  public get totalPrice() {
    return this.cart.products.reduce((prev, product) => prev + product.price, 0);
  }

  public removeItem(index: number) {
    this.cart.removeItem(index);
    this.toast.success("Termék sikeresen eltávolítva a kosárból!");
  }

  public checkout() {
    this.cart.checkout(+this.deskId).subscribe((result) => {
      this.cart.clear()
      window.location.replace(result.url)
    })
  }
}
