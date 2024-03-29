import { Component } from '@angular/core';
import { ProductModel } from '../models/product-model';
import { ProductService } from '../services/product.service';
import { UserModel } from '../models/user-model';
import { AuthService } from '../services/auth.service';


@Component({
  selector: 'app-main-window',
  templateUrl: './main-window.component.html',
  styleUrls: ['./main-window.component.css']
})
export class MainWindowComponent {
  public products: ProductModel[] = []
  public initialCarouselItem = 0;

  constructor(private productService: ProductService, public authService: AuthService) {
    this.loadProducts()
  }


  private loadProducts() {
    this.productService.listProducts().subscribe((products) => {
      this.products = products;
      this.initialCarouselItem = Math.floor(Math.random() * products.length);
    })
  }

}
