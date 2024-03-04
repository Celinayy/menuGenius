import { Component, OnInit } from '@angular/core';
import { CartService } from '../services/cart.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})

export class CartComponent implements OnInit {
  public deskId = "";
  public userId: number = -1;

  constructor(
    public cartService: CartService, 
    private toastr: ToastrService,
    ) {}
  
  ngOnInit(): void {
    this.cartService.loadCartData();
  }

  public get totalPrice() {
    return this.cartService.cartProducts.reduce((prev, product) => prev + product.price, 0);
  }

  public removeItem(index: number) {
    this.cartService.removeItem(index);
    this.toastr.success('A termék törölve!', 'Siker');
  }

  public checkout() {
    this.cartService.checkout(+this.deskId).subscribe((result) => {
      this.cartService.clear()
      window.location.replace(result.url)
    })
  }
}
