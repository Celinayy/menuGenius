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
import { ToastrService } from 'ngx-toastr';
import { Allergen } from '../models/allergen.model';
import { AllergenService } from '../services/allergen.service';
import { AuthService } from '../services/auth.service';
import { User } from '../models/user.model';
import { timeout, catchError, of } from 'rxjs';

@Component({
  selector: 'app-drink-menu',
  templateUrl: './drink-menu.component.html',
  styleUrls: ['./drink-menu.component.css'],
})

export class DrinkMenuComponent implements OnInit {
  public user: User | null = null;
  drinkProducts: Product[] = [];
  drinkCategories: Category[] = [];
  category: Category | null = null;
  tempProducts:Product[] = [];
  
  searchKey: string = "";
  searchChar: string = '';
  public search = new BehaviorSubject<string>("");
  searchTerm: string = '';
  
  currentPage: number = 1;
  drinksSlice: Product[] = [];
  productPerSlice: number = 4;
  filterOptions: { searchTerm: string, category: Category | null, selectedAllergens: { [key: number]: boolean } } = {
    searchTerm: '',
    category: null,
    selectedAllergens: {}
  };

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  
  constructor(
    private productService: ProductService,
    private modalService: NgbModal,
    private cartService: CartService,
    private toastrService: ToastrService,
    private allergenService: AllergenService,
    private authService: AuthService
    ) {}

  ngOnInit(): void {
    this.loadDrinkProducts();
    this.loadCategories();

    this.search.subscribe((searchTerm: string) => {
      this.searchTerm = searchTerm;
      this.combineFilters();
      this.resetPageSlice();
    });
  }

  combineFilteredProducts(allergenFiltered: Product[], charFiltered: Product[], categoryFiltered: Product[]): Product[] {
    const mergedProducts = [...new Set([...allergenFiltered, ...charFiltered, ...categoryFiltered])];
    mergedProducts.sort((a, b) => a.id - b.id);
    return mergedProducts;
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

  combineFilters(): void {
    let filteredProducts = [...this.drinkProducts];
  
    if (this.category) {
      filteredProducts = filteredProducts.filter(product => !this.category || product.category.id === this.category.id);
    }
  
    if (this.searchTerm.trim() !== '') {
      filteredProducts = filteredProducts.filter((product) => product.name.toLowerCase().includes(this.searchTerm.toLowerCase()));
    }  
 
    this.tempProducts = filteredProducts;
    this.resetPageSlice();
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
    this.toastrService.success('A termék a kosárhoz adva!')
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

  resetPageSlice() {
    this.paginator.pageIndex = 0;
    this.drinksSlice = this.tempProducts.slice(0, 4);
  }
}