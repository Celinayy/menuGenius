import { Component, OnInit } from '@angular/core';
import { Product } from '../models/product.model';
import { ProductService } from '../services/product.service';
import { Category } from '../models/category.model';
import { CategoryService } from '../services/category.service';
import { AuthService } from '../services/auth.service';
import { combineLatest } from 'rxjs';

@Component({
  selector: 'app-food-menu',
  templateUrl: './food-menu.component.html',
  styleUrls: ['./food-menu.component.css'],
})
export class FoodMenuComponent implements OnInit {
  searchKey: string = "";
  public searchTerm: string = '';
  public category: Category | null = null;

  foods: Product[] = [];
  public categories: Category[] = [];

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService, 
    public authService: AuthService,
    ) {
      
      this.productService.search.subscribe((val: any) =>{
        this.searchKey = val;
      })
  }
    
  ngOnInit() {
    combineLatest([
      this.productService.listFoodProducts()
    ]).subscribe(([foods]) => {
      this.foods = foods;
      this.loadCategories();
    });
  }
  
  private loadCategories() {
    const foodCategories: Category[] = [];
  
    this.foods.forEach((food) => {
      if (!foodCategories.some(category => category.id === food.category.id)) {
        foodCategories.push(food.category);
      }
    });
  
    this.categories = foodCategories;
  }
  search() {
    this.productService.search.next(this.searchTerm);
  }

  public get filterCategory() {
    return this.productService.foodProducts.filter((p) => {
      if(!this.category) return true;
      return p.category.id === this.category.id;
    })
  }


}
