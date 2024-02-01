import { Component } from '@angular/core';
import { ProductService } from '../services/product.service';
import { FavouriteService } from '../services/favourite.service';
import { ProductModel } from '../models/product-model';

@Component({
  selector: 'app-favourite-food-window',
  templateUrl: './favourite-food-window.component.html',
  styleUrls: ['./favourite-food-window.component.css']
})
export class FavouriteFoodWindowComponent {
  private allProducts: ProductModel[] = [];

  constructor(private productService: ProductService, private favoriteService: FavouriteService) {
    this.productService.listProducts().subscribe((products) => {
      this.allProducts = products;
    })
  }

  public get favouriteProducts() {
    return this.allProducts.filter((product) => {
      return this.favoriteService.isFavourite(product);
    });
  }
}
