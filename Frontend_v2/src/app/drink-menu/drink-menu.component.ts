import { Component, ViewChild, OnInit } from '@angular/core';
import { Product } from '../models/product.model';
import { ProductService } from '../services/product.service';
import { Category } from '../models/category.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ProductModalComponent } from '../modals/product-modal/product-modal.component';
import { CartService } from '../services/cart.service';
import { PageEvent } from '@angular/material/paginator';
import { BehaviorSubject } from 'rxjs';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-drink-menu',
  templateUrl: './drink-menu.component.html',
  styleUrls: ['./drink-menu.component.css']
})

export class DrinkMenuComponent implements OnInit {
  drinkCategories: Category[] = [];
  searchKey: string = "";
  searchChar: string = '';
  category: Category | null = null;
  drinkProducts: Product[] = [];
  tempProducts: Product[] = [];
  currentPage: number = 1;
  drinksSlice: Product[] = [];
  productPerSlice: number = 4;
  public search = new BehaviorSubject<string>("");
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;

  constructor(
    private productService: ProductService,
    private modalService: NgbModal,
    private cartService: CartService,
    ) {}
    
    ngOnInit(): void {
      this.loadDrinkProducts();
      this.loadCategories();
  
      this.search.subscribe((searchTerm: string) => {
        this.tempProducts = this.filterProducts(searchTerm);
        this.currentPage = 1;
        this.updatePageSlice();
      });
    }
    private loadDrinkProducts() {
      this.productService.listDrinkProducts().subscribe((products) => {
        this.drinkProducts = products;
        this.tempProducts = [...this.drinkProducts];
        this.updatePageSlice();
      });
    }
  
    private loadCategories() {
      this.productService.listDrinkProducts().subscribe((drinks) => {
        drinks.forEach((drink) => {
          if (!this.drinkCategories.some(category => category.id === drink.category.id)) {
            this.drinkCategories.push(drink.category);
          }
        });
      });
    }
  
    private filterProducts(searchTerm: string): Product[] {
      return this.drinkProducts.filter((product) =>
        product.name.toLowerCase().includes(searchTerm.toLowerCase())
      );
    }
  
    public onCategoryChange() {
      this.searchChar = "";
      this.tempProducts = [];
      for (let product of this.drinkProducts) {
        if (!this.category || product.category.id === this.category.id) {
          this.tempProducts.push(product);
        }
      }
      this.paginator.pageIndex = 0;
      this.drinksSlice = this.tempProducts.slice(0, 4);
    }
  
    searchProduct() {
      this.search.next(this.searchChar);
    }
  
    showDetails(product: Product) {
      const modalRef = this.modalService.open(ProductModalComponent, {size: 'lg', centered: true, animation: true, keyboard: true});
      modalRef.componentInstance.product = product;
    }
  
    public addToCart(product: Product) {
      this.cartService.addProduct(product);
    }
    
    onPageChange(event: PageEvent) {
      this.currentPage = event.pageIndex + 1;
      this.updatePageSlice();
    }
  
    private updatePageSlice() {
      const startIndex = (this.currentPage - 1) * this.productPerSlice;
      const endIndex = Math.min(startIndex + this.productPerSlice, this.tempProducts.length);
      this.drinksSlice = this.tempProducts.slice(startIndex, endIndex);
    }
  }
  