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
  selector: 'app-food-menu',
  templateUrl: './food-menu.component.html',
  styleUrls: ['./food-menu.component.css'],
})

export class FoodMenuComponent implements OnInit {
  public user: User | null = null;
  foodProducts: Product[] = [];
  foodCategories: Category[] = [];
  category: Category | null = null;
  allergens: Allergen[] = [];
  tempProducts:Product[] = [];
  
  searchKey: string = "";
  searchChar: string = '';
  selectedAllergens: { [key: number]: boolean } = {};
  public search = new BehaviorSubject<string>("");
  searchTerm: string = '';
  
  currentPage: number = 1;
  foodsSlice: Product[] = [];
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
    this.checkUser();
    this.loadFoodProducts();
    this.loadCategories();
    this.getAllergens();
    this.loadSelectedAllergensFromStorage();

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
  
  private loadFoodProducts() {
    this.productService.listFoodProducts().subscribe((products) => {
      this.foodProducts = products;
      this.tempProducts = [...this.foodProducts];
      this.updatePageSlice();
    });
  }
  
  private loadCategories() {
    this.productService.listFoodProducts().subscribe((foods) => {
      foods.forEach((food) => {
        if (!this.foodCategories.some(category => category.id === food.category.id)) {
          this.foodCategories.push(food.category);
        }
      });
    });
  }

  private getAllergens() {
    this.allergenService.getAllAllergen().subscribe(
      allergens => {
        this.allergens = allergens;
        allergens.forEach(allergen => {
          if (allergen.code.toString().endsWith('00')) allergen.parent = true;
          else allergen.parent = false;
        });
      },
      error => {
        console.error('Error fetching allergens:', error);
      }
    );
  }

  combineFilters(): void {
    let filteredProducts = [...this.foodProducts];
  
    if (this.category) {
      filteredProducts = filteredProducts.filter(product => !this.category || product.category.id === this.category.id);
    }
  
    if (this.searchTerm.trim() !== '') {
      filteredProducts = filteredProducts.filter((product) => product.name.toLowerCase().includes(this.searchTerm.toLowerCase()));
    }  
    if (this.selectedAllergens) {
      filteredProducts = filteredProducts.filter((product) => product.ingredients.every(ingredient => ingredient.allergens.every(allergen => this.selectedAllergens[allergen.id])));
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
    this.foodsSlice = this.tempProducts.slice(startIndex, endIndex);
  }

  toggleAllergen(allergenId: number): void {
    this.selectedAllergens[allergenId] = !this.selectedAllergens[allergenId];
    this.saveSelectedAllergensToStorage();
    this.combineFilters();
  }

  loadSelectedAllergensFromStorage(): void {
    if (this.user){
      const selectedAllergensString = localStorage.getItem(`selectedAllergens_${this.user.id}`);
      if (selectedAllergensString) {
        this.selectedAllergens = JSON.parse(selectedAllergensString);
      }
    } else {
      const selectedAllergensString = sessionStorage.getItem('selectedAllergens');
      if (selectedAllergensString) {
        this.selectedAllergens = JSON.parse(selectedAllergensString);
      } else {
        this.setAllAllergensActive();
      }
    }
  }

  setAllAllergensActive(): void {
    for (const allergen of this.allergens) {
      this.selectedAllergens[allergen.id] = true;
    }
    this.saveSelectedAllergensToStorage();
  }

  saveSelectedAllergensToStorage(): void {
    if (this.user){
      localStorage.setItem(`selectedAllergens_${this.user.id}`, JSON.stringify(this.selectedAllergens));
    } else {
      sessionStorage.setItem('selectedAllergens', JSON.stringify(this.selectedAllergens));
    }
  }

  resetPageSlice() {
    this.paginator.pageIndex = 0;
    this.foodsSlice = this.tempProducts.slice(0, 4);
  }

  checkUser() {
    this.authService.getUser().pipe(
      timeout(5000),
      catchError(error => of(null))
    ).subscribe((user) =>{
      if (user) {
        this.user = user;
        const storageData = localStorage.getItem(`selectedAllergens_${this.user.id}`);
        console.log('storageData-login', storageData)
        if (storageData) {
          this.selectedAllergens = JSON.parse(storageData);
        } else {
          this.setAllAllergensActive();
        }
      } else {
          const storageData = sessionStorage.getItem('selectedAllergens');
          console.log('User', user)
          console.log('storageData-nologin', storageData)
          if (storageData) {
            this.selectedAllergens = JSON.parse(storageData);
          } else {
            this.setAllAllergensActive();
          }
      }
    });
  }
}