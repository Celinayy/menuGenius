import { Component, OnInit } from '@angular/core';
import { Product } from '../models/product.model';
import { ProductService } from '../services/product.service';
import { Category } from '../models/category.model';
import { CategoryService } from '../services/category.service';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-drink-menu',
  templateUrl: './drink-menu.component.html',
  styleUrls: ['./drink-menu.component.css']
})
export class DrinkMenuComponent {
  searchKey: string = "";
  public searchTerm: string = '';
  public category : Object | null = null;

  public categories: Category[] = [];
  drinks: Product[] = [];

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService, 
    public authService: AuthService,
    ) {
      this.loadCategories();

      this.productService.search.subscribe((val: any) =>{
        this.searchKey = val;
      })
    }

  ngOnInit() {
    this.productService.listDrinkProducts().subscribe(drinks => {
      this.drinks = drinks;
      //this.slides = this.products.map(product => ({ img: 'data:image/png;base64,' + product.image.data }));
    });
  }

  private loadCategories() {
    this.categoryService.getAllCategory().subscribe((categories) => {
      this.categories = categories;
    })
  }

  search() {
    this.productService.search.next(this.searchTerm);
  }

  public get filterCategory() {
    return this.productService.drinkProducts.filter((p) => {
      if(!this.category) return true;
      return p.category === this.category;
    })
  }


}
