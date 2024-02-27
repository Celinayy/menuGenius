import { Component, OnInit } from '@angular/core';
import { Product } from '../models/product.model';
import { ProductService } from '../services/product.service';
import { Category } from '../models/category.model';
import { combineLatest } from 'rxjs';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { ProductModalComponent } from '../product-modal/product-modal.component';

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

  showDetails(product: Product) {
    const modalRef = this.modalService.open(ProductModalComponent);
    modalRef.componentInstance.product = product;
  }
}
