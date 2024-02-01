import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ProductModel } from '../models/product-model';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class FavouriteService {

  private favProducIds: number[] = [];
  private url = "http://localhost:8000/api"

  constructor(private connection: HttpClient, private toast: ToastrService) { }

  private loadFavourites() {
    return this.connection.get<ProductModel[]>(`${this.url}/products/`)
  }

  public addToFavourite(product: ProductModel) {
    this.favProducIds.push(product.id)

    this.connection.post<{ message: string }>(`${this.url}/product/${product.id}/addToFavorites`, {}, {
      headers: {
        "Authorization": `Bearer ${localStorage.getItem("token")}`,
      },
    }).subscribe((result) => {
      this.toast.success(result.message);
    });
  }

  public removeFromFavourite(product: ProductModel) {
    const result: number[] = []
    for (const favProducId of this.favProducIds) {
      if (favProducId != product.id) {
        result.push(favProducId)
      }
    }
    this.favProducIds = result;

    return this.connection.post<{ message: string }>(`${this.url}/product/${product.id}/removeFromFavorites`, {}, {
      headers: {
        "Authorization": `Bearer ${localStorage.getItem("token")}`,
      },
    }).subscribe((result) => {
      this.toast.success(result.message);
    });
  }

  public isFavourite(product: ProductModel) {
    return this.favProducIds.includes(product.id)
  }


}
