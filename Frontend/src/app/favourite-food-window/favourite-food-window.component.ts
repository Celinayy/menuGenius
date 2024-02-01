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
  constructor(private productService: ProductService, private favoriteService: FavouriteService) {}

  public get favouriteProducts() {
    return this.productService.products.filter((product) => {
      return this.favoriteService.isFavourite(product);
    });
  }
}
