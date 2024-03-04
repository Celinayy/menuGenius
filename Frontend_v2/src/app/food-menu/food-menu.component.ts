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
  tempProducts: Product[] = [];
  currentPage: number = 1;
  foodsSlice: Product[] = [];
  productPerSlice: number = 4;
  public search = new BehaviorSubject<string>("");
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  
  constructor(
    private productService: ProductService,
    private modalService: NgbModal,
    private cartService: CartService,
    private toastrService: ToastrService
    ) {}

  ngOnInit(): void {
    this.loadFoodProducts();
    this.loadCategories();

    this.search.subscribe((searchTerm: string) => {
      this.tempProducts = this.filterProducts(searchTerm);
      this.currentPage = 1;
      this.updatePageSlice();
    });
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
    return this.foodProducts.filter((product) =>
      product.name.toLowerCase().includes(searchTerm.toLowerCase())
    );
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
}
