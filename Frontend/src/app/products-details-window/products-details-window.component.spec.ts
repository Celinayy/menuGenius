import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductsDetailsWindowComponent } from './products-details-window.component';

describe('ProductsDetailsWindowComponent', () => {
  let component: ProductsDetailsWindowComponent;
  let fixture: ComponentFixture<ProductsDetailsWindowComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProductsDetailsWindowComponent]
    });
    fixture = TestBed.createComponent(ProductsDetailsWindowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
