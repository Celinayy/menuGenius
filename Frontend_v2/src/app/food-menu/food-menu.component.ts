import { Component, ViewChild, OnInit } from '@angular/core';
import { Product } from '../models/product.model';
import { ProductService } from '../services/product.service';
import { Category } from '../models/category.model';
import { combineLatest } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ProductModalComponent } from '../modals/product-modal/product-modal.component';
import { CartService } from '../services/cart.service';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-food-menu',
  templateUrl: './food-menu.component.html',
  styleUrls: ['./food-menu.component.css'],
})

export class FoodMenuComponent implements OnInit {
  searchKey: string = "";
  public searchTerm: string = '';
  public category: Category | null = null;
  public product!: Product;
  currentPage: number = 1;
  public foodsSlice: Product[] = [];
  
  foods: Product[] = [];
  public categories: Category[] = [];
  
  constructor(
    private productService: ProductService,
    private modalService: NgbModal,
    private cartService: CartService,
    ) {
      this.productService.search.subscribe((val: any) =>{
        this.searchKey = val;
      });
    }
    
  // ngOnInit() {
  //   combineLatest([
  //     this.productService.listFoodProducts()
  //   ]).subscribe(([foods]) => {
  //     this.foods = foods;
  //     this.loadCategories();
  //     this.pageSlice = this.filterCategory.slice(0, 4);
  //   });
  // }

  ngOnInit() {
    this.loadFoods();
    this.loadCategories();
  }

  private loadFoods() {
    this.productService.listFoodProducts().subscribe((products) => {
      this.foods = products;
      this.foodsSlice = this.filterCategory.slice(0, 4);
    });
  }
  
  private loadCategories() {
    const foodCategories: Category[] = [];
    
    this.productService.listFoodProducts().subscribe((foods) => {
      foods.forEach((food) => {
        if (!foodCategories.some(category => category.id === food.category.id)) {
          foodCategories.push(food.category);
        }
      });
  
      this.categories = foodCategories;
    });
  }

  search() {
    this.productService.search.next(this.searchTerm);
  }

  public get filterCategory() {
    return this.foods.filter((food) => {
      const matchesCategory = !this.category || food.category === this.category;
      const matchesSearchTerm = !this.searchTerm || food.name.toLowerCase().includes(this.searchTerm.toLowerCase());
      return matchesCategory && matchesSearchTerm;
    });
  }

  // public get filterCategory() {
  //   return this.productService.foodProducts.filter((food) => {
  //     if(!this.category) return true;
  //     return food.category === this.category;
  //   })
  // }

  showDetails(product: Product) {
    const modalRef = this.modalService.open(ProductModalComponent, {size: 'lg', centered: true, animation: true, keyboard: true});
    modalRef.componentInstance.product = product;
  }

  public addToCart(product: Product) {
    this.cartService.addProduct(product);
  }
  
  onPageChange(event: PageEvent) {
    const startIndex = event.pageIndex * event.pageSize;
    let endIndex = startIndex + event.pageSize;

    if (endIndex > this.filterCategory.length) {
      endIndex = this.filterCategory.length;
    }
    this.foodsSlice = this.filterCategory.slice(startIndex, endIndex);
  }

  // prevPage() {
  //   if (this.currentPage > 1) {
  //     console.log(this.currentPage, this.totalPages())

  //       this.currentPage--;
  //   }
  // }

  // nextPage() {
  //   if (this.currentPage < this.totalPages()) {
  //     console.log(this.currentPage, this.totalPages())
  //       this.currentPage++;
  //   }
  // }

  // totalPages() {
  //   return Math.ceil(this.foods.length / this.imgInOneSlide);
  // }

    // public get filterCategory() {
  //   const startIndex = (this.currentPage - 1) * 3;
  //   const endIndex = this.currentPage * 3;
    
  //   let filteredProducts = this.productService.foodProducts;
  
  //   if (this.category) {
  //     filteredProducts = filteredProducts.filter(product =>
  //       product.category === this.category
  //     );
  
  //     this.currentPage = 1;
  //   }
  
  //   return filteredProducts.slice(startIndex, endIndex);
  // }
  

  // public get filterCategory() {
  //   let startIndex = (this.currentPage - 1) * this.imgInOneSlide;

  //   const filteredFoods = this.productService.foodProducts.filter((p) => {
  //     if(!this.category) return true;
  //     return p.category.id === this.category.id;
  //   })

  //   const endIndex = this.currentPage * this.imgInOneSlide;

  //   return filteredFoods.slice(startIndex, endIndex);

  // }

  // public get filterCategory() {
  //   let startIndex = 0;

  //   //const startIndex = (this.currentPage - 1) * 3;
  //   //const endIndex = this.currentPage * 3;
  //   return this.productService.foodProducts.filter((p) => {
  //     if(!this.category) return true;
  //     return p.category.id === this.category.id;
  //   })
  //   .slice(startIndex, endIndex);
  // }



}
