import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductsWindowComponent } from './products-window.component';

describe('ProductsWindowComponent', () => {
  let component: ProductsWindowComponent;
  let fixture: ComponentFixture<ProductsWindowComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProductsWindowComponent]
    });
    fixture = TestBed.createComponent(ProductsWindowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
