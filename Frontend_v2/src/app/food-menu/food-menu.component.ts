import { Component, OnInit } from '@angular/core';
import { Product } from '../models/product.model';
import { ProductService } from '../services/product.service';
import { Category } from '../models/category.model';
import { combineLatest } from 'rxjs';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { ProductModalComponent } from '../product-modal/product-modal.component';

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
    private modalService: NgbModal,
    private modalConfig: NgbModalConfig
    ) {
      this.productService.search.subscribe((val: any) =>{
        this.searchKey = val;
      }),
      this.modalConfig.backdrop = 'static';
      this.modalConfig.keyboard = true;
      this.modalConfig.animation = true,
      this.modalConfig.size = 'lg',
      this.modalConfig.centered = true
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

  showDetails(product: Product) {
    const modalRef = this.modalService.open(ProductModalComponent);
    modalRef.componentInstance.product = product;
  }
}
