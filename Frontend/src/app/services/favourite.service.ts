import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { ProductModel } from '../models/product-model';
import { ToastrService } from 'ngx-toastr';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root'
})
export class FavouriteService {

  private favProducIds: number[] = [];
  private url:string = `${environment.apiUrl}/api`



  constructor(
    private connection: HttpClient, 
    private toast: ToastrService) {
    this.loadFavourites().subscribe((result) => {
      this.favProducIds  = result.favorites.map((product) => {
        return product.id;
      });
    })
  }

  private loadFavourites() {
    return this.connection.get<{favorites: ProductModel[]}>(`${this.url}/product/userFavorites`, {
      headers: {
        "Authorization": `Bearer ${localStorage.getItem("token")}`,
      },
    })
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
