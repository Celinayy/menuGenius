import { Component, ViewChild, OnInit, Input } from '@angular/core';
import { Product } from '../models/product.model';
import { ProductService } from '../services/product.service';
import { Category } from '../models/category.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ProductModalComponent } from '../modals/product-modal/product-modal.component';
import { CartService } from '../services/cart.service';
import { PageEvent } from '@angular/material/paginator';
import { BehaviorSubject, Subject } from 'rxjs';
import { MatPaginator } from '@angular/material/paginator';
import { ToastrService } from 'ngx-toastr';
import { Allergen } from '../models/allergen.model';
import { AllergenService } from '../services/allergen.service';
import { MatChipsModule } from '@angular/material/chips';
import { MatChipListbox } from '@angular/material/chips';
import { AuthService } from '../services/auth.service';
import { User } from '../models/user.model';

@Component({
  selector: 'app-food-menu',
  templateUrl: './food-menu.component.html',
  styleUrls: ['./food-menu.component.css'],
})

export class FoodMenuComponent implements OnInit {
  foodCategories: Category[] = [];
  searchKey: string = "";
  searchChar: string = '';
  category: Category | null = null;
  foodProducts: Product[] = [];
  categoryFilteredProducts: Product[] = [];
  searchFilteredProducts: Product[] = [];
  allergenFilteredProducts: Product[] = [];
  tempProducts:Product[] = [];
  currentPage: number = 1;
  foodsSlice: Product[] = [];
  productPerSlice: number = 4;
  public search = new BehaviorSubject<string>("");
  allergens: Allergen[] = [];
  selectedAllergens: { [key: number]: boolean } = {};
  public user: User | null = null;

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
    this.loadFoodProducts();
    this.loadCategories();
    this.getAllergens();
    
    this.search.subscribe((searchTerm: string) => {
      this.tempProducts = this.filterProducts(searchTerm);
      this.currentPage = 1;
      this.updatePageSlice();
    });
    
    this.authService.getUser().subscribe((user) =>{
      if (user) {
        this.user = user;
      }
      this.loadSelectedAllergensFromStorage()
      this.getFilterAllergen();
    });
  }

  getAllergens(): void {
    this.allergenService.getAllAllergen().subscribe(
      allergens => {
        this.allergens = allergens;
        this.allergens.forEach(allergen => {
          this.selectedAllergens[allergen.id] = true;
        });
      },
      error => {
        console.error('Error fetching allergens:', error);
      }
    );
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

  private filterProducts(searchTerm: string): Product[] {
    this.searchFilteredProducts = this.tempProducts.filter((product) => product.name.toLowerCase().includes(searchTerm.toLowerCase()));
    return this.searchFilteredProducts;
  }

  public onCategoryChange() {
    this.searchChar = "";
    this.tempProducts = [];
    for (const product of this.foodProducts) {
      if (!this.category || product.category.id === this.category.id) {
        this.tempProducts.push(product);
      }
    }
    this.paginator.pageIndex = 0;
    this.foodsSlice = this.tempProducts.slice(0, 4);
  }

  onAllergenChange(): void {
    this.getFilterAllergen();
  }

  getFilterAllergen(): void {
    console.log(this.selectedAllergens)
    this.tempProducts = this.foodProducts.filter(product =>
      product.ingredients.every(ingredient =>
        ingredient.allergens.every(allergen => this.selectedAllergens[allergen.id])
        )
        );
      console.log(this.tempProducts)
      this.paginator.pageIndex = 0;
    this.foodsSlice = this.tempProducts.slice(0, 4);
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
    this.getFilterAllergen();
  }

  loadSelectedAllergensFromStorage(): void {
    if (this.user){
      console.log('user-lsafs-if:', this.user.id)
      const selectedAllergensString = localStorage.getItem(`selectedAllergens_${this.user.id}`);
      console.log('sAS-lsafs-if:', selectedAllergensString)
      if (selectedAllergensString) {
        this.selectedAllergens = JSON.parse(selectedAllergensString);
      }
    } else {
      console.log('user-lsafs-else:', this.user)
      const selectedAllergensString = sessionStorage.getItem('selectedAllergens');
      console.log('sAS-lsafs-else:', selectedAllergensString)
      if (selectedAllergensString) {
        this.selectedAllergens = JSON.parse(selectedAllergensString);
        console.log('sAS-lsafs-else-if:', this.selectedAllergens)
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
}
