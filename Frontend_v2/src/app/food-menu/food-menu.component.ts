import { Component, OnInit } from '@angular/core';
import { Product } from '../models/product.model';
import { ProductService } from '../services/product.service';
import { Category } from '../models/category.model';
import { combineLatest } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ProductModalComponent } from '../modals/product-modal/product-modal.component';
import { CartService } from '../services/cart.service';
import { ToastrService } from 'ngx-toastr';

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

  foods: Product[] = [];
  public categories: Category[] = [];

  constructor(
    private productService: ProductService,
    private modalService: NgbModal,
    private cartService: CartService,
    private toast: ToastrService,

    ) {
      this.productService.search.subscribe((val: any) =>{
        this.searchKey = val;
      });
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
    const modalRef = this.modalService.open(ProductModalComponent, {size: 'lg', centered: true, animation: true, keyboard: true});
    modalRef.componentInstance.product = product;
  }

  public addToCart(product: Product) {
    this.toast.success("Kosárhoz adva!")
    this.cartService.addProduct(product);
 }
}
