import { Component, OnInit } from '@angular/core';
import { ProductService } from '../services/product.service';
import { CategoryModel } from '../models/category-model';
import { CategoriesService } from '../services/categories.service';
import { AuthService } from '../services/auth.service';
import { ProductModel } from '../models/product-model';

@Component({
  selector: 'app-drinks-window',
  templateUrl: './drinks-window.component.html',
  styleUrls: ['./drinks-window.component.css']
})
export class DrinksWindowComponent {

  public products: ProductModel[] = []

  constructor(
    private productService: ProductService,
    public authService: AuthService,
    ) {

    this.productService.listProducts().subscribe(() => {
      this.products = this.productService.listDrinks()
    })

  };


}
