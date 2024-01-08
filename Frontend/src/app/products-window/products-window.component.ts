import { Component } from '@angular/core';
import { ProductModel } from '../models/product-model';
import { ProductService } from '../services/product.service';

@Component({
  selector: 'app-products-window',
  templateUrl: './products-window.component.html',
  styleUrls: ['./products-window.component.css']
})
export class ProductsWindowComponent {

  public products: ProductModel[] = []

  constructor(private productService: ProductService) {
    this.loadProducts()
  }

  private loadProducts() {
    this.productService.listProducts().subscribe((products) => {
      this.products = products
    })
  }

}
