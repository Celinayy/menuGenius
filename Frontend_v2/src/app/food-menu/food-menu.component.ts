import { Component, OnInit } from '@angular/core';
import { Product } from '../models/product.model';
import { ProductService } from '../services/product.service';

@Component({
  selector: 'app-food-menu',
  templateUrl: './food-menu.component.html',
  styleUrls: ['./food-menu.component.css']
})
export class FoodMenuComponent implements OnInit {
  products: Product[] = [];

  constructor(private productService: ProductService) { }

  ngOnInit() {
    this.productService.listFoodProducts().subscribe(products => {
      this.products = products;

      //console.log('Product images:', this.products.map(product => product.image.data));

      this.slides = this.products.map(product => ({ img: 'data:image/png;base64,' + product.image.data }));
    });
  }

  slides: { img: string }[] = [];

  // slides = [
  //   { img: 'https://via.placeholder.com/600.png/09f/fff' },
  //   { img: 'https://via.placeholder.com/600.png/021/fff' },
  //   { img: 'https://via.placeholder.com/600.png/321/fff' },
  //   { img: 'https://via.placeholder.com/600.png/422/fff' },
  //   { img: 'https://via.placeholder.com/600.png/654/fff' },
  // ];

  slideConfig = { 
    slidesToShow: 1,
    slidesToScroll: 1,
    variableWidth: true,
    centerMode: true,
    dots: false,
    arrows: true,
    adaptiveHeight: true,
    variableHeight: true,
    responsive: [
      {
        breakpoint: 768,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
          variableWidth: false,
        }
      }
    ]    
  };

  addSlide() {
    this.slides.push({ img: 'http://placehold.it/350x150/777777' });
  }

  removeSlide() {
    this.slides.length = this.slides.length - 1;
  }

  slickInit(e: any) {
    console.log('slick initialized');
  }
  breakpoint(e: any) {
    console.log('breakpoint');
  }
  afterChange(e: any) {
    console.log('afterChange');
  }

  beforeChange(e: any) {
    console.log('beforeChange');
  }
}
