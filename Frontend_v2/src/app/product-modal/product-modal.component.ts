import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ProductService } from '../services/product.service';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../models/product.model';
import { ToastrService } from 'ngx-toastr';
import { CartService } from '../services/cart.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-product-detail-modal',
  templateUrl: './product-modal.component.html',
  styleUrls: ['./product-modal.component.css']
})

export class ProductModalComponent {
  @Input() product!: Product;

  constructor(
    private activatedRoute: ActivatedRoute,
    public productService: ProductService,
    private toast: ToastrService,
    private cartService: CartService,
    private _location: Location,
    public activeModal: NgbActiveModal
  ) {
    // this.activatedRoute.paramMap.subscribe((params) => {
    //   this.productService.loadProductById(parseInt(params.get("id") as string)).subscribe((product) => {
    //     this.product = product;
    //   })
    // })
  }

  public get allergens() {
    if (!this.product) {
      return []
    }
    const allergensWithDuplicates = this.product.ingredients.flatMap((ingredient) => {
      return ingredient.allergens.map((allergen) => {
        return allergen.name
      })
    })
    return Array.from(new Set(allergensWithDuplicates))
  }

  public addToCart() {
     this.toast.success("Kosárhoz adva!")
     this.cartService.addProduct(this.product);
  }

  closeModal() {
    this.activeModal.close();
  }

}
