import { Component, OnInit } from '@angular/core';
import { ProductModel } from '../models/product-model';
import { ProductService } from '../services/product.service';
import { CategoryModel } from '../models/category-model';
import { CategoriesService } from '../services/categories.service';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-products-window',
  templateUrl: './products-window.component.html',
  styleUrls: ['./products-window.component.css']
})
export class ProductsWindowComponent {
  public categories: CategoryModel[] = [];

  searchKey: string = "";
  public searchTerm: string = '';
  public category_id : number | null = null;

  constructor(
    private productService: ProductService,
    private categoryService: CategoriesService,
    public authService: AuthService,
    ) {
    this.loadCategories();

    this.productService.search.subscribe((val: any) =>{
      this.searchKey = val;
    })

  };

  private loadCategories() {
    this.categoryService.getAllCategory().subscribe((categories) => {
      this.categories = categories;
    })
  }


  search() {
    this.productService.search.next(this.searchTerm);
  }

  public get filterCategory() {
    return this.productService.products.filter((p) => {
      if(!this.category_id) return true;
      return p.category_id === this.category_id;
    })
  }
}
