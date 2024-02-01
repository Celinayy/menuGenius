import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ProductModel } from '../models/product-model';

@Injectable({
  providedIn: 'root'
})
export class FavouriteService {

  private favProducIds: number[] = [];
  private url = "http://localhost:8000/api/product"



  constructor(private connection: HttpClient) { }

  private loadFavourites() {
    return this.connection.get<ProductModel[]>(`${this.url}`)
  }

  public addToFavourite(product: ProductModel) {
    return this.favProducIds.push(product.id)
  }

  public removeFromFavourite(product: ProductModel) {
    const result: number[] = []
    for (const favProducId of this.favProducIds) {
      if (favProducId != product.id) {
        result.push(favProducId)
      }
    }
    this.favProducIds = result
  }

  public isFavourite(product: ProductModel) {
    return this.favProducIds.includes(product.id)
  }


}
