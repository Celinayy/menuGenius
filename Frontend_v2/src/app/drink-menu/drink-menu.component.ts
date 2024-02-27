import { Component, OnInit } from '@angular/core';
import { Product } from '../models/product.model';
import { ProductService } from '../services/product.service';
import { Category } from '../models/category.model';
import { CategoryService } from '../services/category.service';
import { AuthService } from '../services/auth.service';
import { combineLatest } from 'rxjs';

@Component({
  selector: 'app-drink-menu',
  templateUrl: './drink-menu.component.html',
  styleUrls: ['./drink-menu.component.css']
})
export class DrinkMenuComponent {
  searchKey: string = "";
  public searchTerm: string = '';
  public category: Category | null = null;

  drinks: Product[] = [];
  public categories: Category[] = [];

  constructor(
    private productService: ProductService,
    public authService: AuthService,
    ) {
    this.productService.search.subscribe((val: any) =>{
      this.searchKey = val;
    })
  }
    
  ngOnInit() {
    combineLatest([
      this.productService.listDrinkProducts()
    ]).subscribe(([drinks]) => {
      this.drinks = drinks;
      this.loadCategories();
    });
  }
  
  private loadCategories() {
    const drinkCategories: Category[] = [];
  
    this.drinks.forEach((drink) => {
      if (!drinkCategories.some(category => category.id === drink.category.id)) {
        drinkCategories.push(drink.category);
      }
    });
  
    this.categories = drinkCategories;
  }
  search() {
    this.productService.search.next(this.searchTerm);
  }

  public get filterCategory() {
    return this.productService.drinkProducts.filter((p) => {
      if(!this.category) return true;
      return p.category.id === this.category.id;
    })
  }


}
