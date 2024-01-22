import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FavouriteFoodWindowComponent } from './favourite-food-window.component';

describe('FavouriteFoodWindowComponent', () => {
  let component: FavouriteFoodWindowComponent;
  let fixture: ComponentFixture<FavouriteFoodWindowComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FavouriteFoodWindowComponent]
    });
    fixture = TestBed.createComponent(FavouriteFoodWindowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
