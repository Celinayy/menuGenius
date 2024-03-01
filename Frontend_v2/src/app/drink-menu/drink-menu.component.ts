import { Component, OnInit } from '@angular/core';
import { Product } from '../models/product.model';
import { ProductService } from '../services/product.service';
import { Category } from '../models/category.model';
import { combineLatest } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ProductModalComponent } from '../modals/product-modal/product-modal.component';
import { CartService } from '../services/cart.service';

@Component({
  selector: 'app-drink-menu',
  templateUrl: './drink-menu.component.html',
  styleUrls: ['./drink-menu.component.css']
})

export class DrinkMenuComponent implements OnInit {
  searchKey: string = "";
  public searchTerm: string = '';
  public category: Category | null = null;
  public product!: Product;
  currentPage: number = 1;
  public drinksSlice: Product[] = [];


  drinks: Product[] = [];
  public categories: Category[] = [];

  constructor(
    private productService: ProductService,
    private modalService: NgbModal,
    private cartService: CartService,
    ) {
      // this.productService.search.subscribe((val: any) =>{
      //   this.searchKey = val;
      // });
    }
    
  // ngOnInit() {
  //   combineLatest([
  //     this.productService.listDrinkProducts()
  //   ]).subscribe(([drinks]) => {
  //     this.drinks = drinks;
  //     this.loadCategories();
  //   });
  // }

  ngOnInit() {
    this.loadDrinks();
    this.loadCategories();
  }

  private loadDrinks() {
    this.productService.listDrinkProducts().subscribe((drinkProducts) => {
      this.drinks = drinkProducts;
      this.drinksSlice = this.drinks.slice(0, 4);
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
  
  // search() {
  //   this.productService.search.next(this.searchTerm);
  // }

  // public get filterCategory() {
  //   return this.productService.listDrinkProducts().filter((p) => {
  //     if(!this.category) return true;
  //     return p.category.id === this.category.id;
  //   })
  // }

  // public get filterCategory() {
  //   return this.productService.drinkProducts.filter((p) => {
  //     if(!this.category) return true;
  //     return p.category.id === this.category.id;
  //   })
  // }

  showDetails(product: Product) {
    const modalRef = this.modalService.open(ProductModalComponent, {size: 'lg', centered: true, animation: true, keyboard: true});
    modalRef.componentInstance.product = product;
  }

  public addToCart(product: Product) {
    this.cartService.addProduct(product);
  }
}
