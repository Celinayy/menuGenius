import { Component, OnInit } from '@angular/core';
import { ProductService } from '../services/product.service';
import { AuthService } from '../services/auth.service';
import { ProductModel } from '../models/product-model';

@Component({
  selector: 'app-foods-window',
  templateUrl: './foods-window.component.html',
  styleUrls: ['./foods-window.component.css']
})
export class FoodsWindowComponent {

  public products: ProductModel[] = []

  constructor(
    private productService: ProductService,
    public authService: AuthService,
    ) {

    this.productService.listProducts().subscribe(() => {
      this.products = this.productService.listFoods()
    })

  };


}
