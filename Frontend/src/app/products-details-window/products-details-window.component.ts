import { Component } from '@angular/core';
import { ProductService } from '../services/product.service';
import { ActivatedRoute } from '@angular/router';
import { ProductModel } from '../models/product-model';

@Component({
  selector: 'app-products-details-window',
  templateUrl: './products-details-window.component.html',
  styleUrls: ['./products-details-window.component.css']
})
export class ProductsDetailsWindowComponent {

  public product: ProductModel | undefined;

  constructor(
    private activatedRoute: ActivatedRoute,
    public productService: ProductService
  ) {
    this.activatedRoute.paramMap.subscribe((params) => {
      this.productService.loadProductById(parseInt(params.get("id") as string)).subscribe((product) => {
        this.product = product;
      })
    })
  }

  public get allergens() {
    if (!this.product) {
      return []
    }
    const allergensWithDuplicates = this.product.ingredients.flatMap((ingredient) => {
      return ingredient.allergens.map((allergen) => {
        return allergen.name
      })
    })
    return Array.from(new Set(allergensWithDuplicates))
  }

}
