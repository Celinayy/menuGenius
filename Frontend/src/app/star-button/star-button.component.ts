import { Component, Input } from '@angular/core';
import { FavouriteService } from '../services/favourite.service';
import { ProductModel } from '../models/product-model';

@Component({
  selector: 'app-star-button',
  templateUrl: './star-button.component.html',
  styleUrls: ['./star-button.component.css']
})
export class StarButtonComponent {

    @Input({
      required: true
    })

    product!: ProductModel


    constructor(private favouriteService: FavouriteService) {

    }

    get isFavourite() {
      return this.favouriteService.isFavourite(this.product)
    }

    public addToFavouriteList() {
      this.favouriteService.addToFavourite(this.product)
    }

    public removeFromFavouriteList() {
      this.favouriteService.removeFromFavourite(this.product)
    }
}
