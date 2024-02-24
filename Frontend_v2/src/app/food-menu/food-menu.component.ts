import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { interval, Observable, startWith, Subject, switchMap, timer } from 'rxjs';
import { Product } from '../models/product.model';
import { ProductService } from '../services/product.service';
import { Category } from '../models/category.model';
import { CategoryService } from '../services/category.service';
import { AuthService } from '../services/auth.service';
import { Image } from '../models/image.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-food-menu',
  templateUrl: './food-menu.component.html',
  styleUrls: ['./food-menu.component.css'],
})
export class FoodMenuComponent implements OnInit {
  @Input() slides: Image[] = [];
  currentIndex: number = 0;
  //timeoutId?: number;

  searchKey: string = "";
  public searchTerm: string = '';
  public category_id : number | null = null;


  public categories: Category[] = [];
  products: Product[] = [];

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService, 
    public authService: AuthService,
    ) {
      this.loadCategories();

      this.productService.search.subscribe((val: any) =>{
        this.searchKey = val;
      })
    };

  ngOnInit() {
    this.productService.listFoodProducts().subscribe(products => {
      this.products = products;
    });
    //this.resetTimer();
  }

  ngOnDestroy() {
    //window.clearTimeout(this.timeoutId);
  }

  // resetTimer() {
  //   if (this.timeoutId) {
  //     window.clearTimeout(this.timeoutId);
  //   }
  //   this.timeoutId = window.setTimeout(() => this.goToNext(), 3000);
  // }

  goToPrevious(): void {
    const isFirstSlide = this.currentIndex === 0;
    const newIndex = isFirstSlide
      ? this.slides.length - 1
      : this.currentIndex - 1;

    //this.resetTimer();
    this.currentIndex = newIndex;
  }

  goToNext(): void {
    const isLastSlide = this.currentIndex === this.slides.length - 1;
    const newIndex = isLastSlide ? 0 : this.currentIndex + 1;

    //this.resetTimer();
    this.currentIndex = newIndex;
  }

  goToSlide(slideIndex: number): void {
    //this.resetTimer();
    this.currentIndex = slideIndex;
  }

  getCurrentSlideUrl() {
    return `url('data:image/jpeg;base64, ${this.products[this.currentIndex].image.data}')`;
  }
  
  private loadCategories() {
    this.categoryService.getAllCategory().subscribe((categories) => {
      this.categories = categories;
    })
  }

  search() {
    this.productService.search.next(this.searchTerm);
  }

  // public get filterCategory() {
  //   return this.productService.listFoodProducts().filter((p) => {
  //     if(!this.category_id) return true;
  //     return p.category.id === this.category_id;
  //   })
  // }

  goToProductSlide(product: Product): void {
    this.currentIndex = this.products.findIndex(p => p === product);
  }
}