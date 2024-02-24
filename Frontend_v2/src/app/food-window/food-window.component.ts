import { Component, OnInit } from '@angular/core';
import { Product } from '../models/product.model';
import { ProductService } from '../services/product.service';
import { Category } from '../models/category.model';
import { CategoryService } from '../services/category.service';
import { AuthService } from '../services/auth.service';
import { Image } from '../models/image.model';


@Component({
  selector: 'app-food-window',
  templateUrl: './food-window.component.html',
  styleUrl: './food-window.component.css',
})
export class FoodWindowComponent implements OnInit {

  searchKey: string = "";
  public searchTerm: string = '';
  public category_id : number | null = null;

  public categories: Category[] = [];
  products: Product[] = [];

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService, 
    public authService: AuthService,
    ) {
      this.loadCategories();

      this.productService.search.subscribe((val: any) =>{
        this.searchKey = val;
      })
    }

  ngOnInit() {
    this.productService.listFoodProducts().subscribe(products => {
      this.products = products;
      this.slides = this.products.map(product => ({ img: 'data:image/png;base64,' + product.image.data }));
    });
  }

  private loadCategories() {
    this.categoryService.getAllCategory().subscribe((categories) => {
      this.categories = categories;
    })
  }

  search() {
    this.productService.search.next(this.searchTerm);
  }

  slides: { img: string }[] = [];

  slideConfig = {
    slidesToShow: 3,
    slidesToScroll: 1,
    arrows: true,
    asNavFor: '.carousel-small',
    centerMode: true,
    accessibility: true,
    swipeToSlide: true,
    //mobileFirst: true
    //lazyload: 'progressive'
    easing: 'swing',
    focusOnSelect: true,
    centerPadding: 0
    };
  slideNavConfig = {
    slidesToShow: 11,
    slidesToScroll: 5,
    asNavFor: '.carousel',
    dots: false,
    focusOnSelect: true,
    arrows: true,
    centerMode: true,
  };

}