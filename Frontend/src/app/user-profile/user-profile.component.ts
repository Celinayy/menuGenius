import { Component } from '@angular/core';
import { ProductModel } from '../models/product-model';
import { ProductService } from '../services/product.service';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent {
  public initialCarouselItem = 0;
  public products: ProductModel[] = []

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
