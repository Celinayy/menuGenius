import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FoodWindowComponent } from './food-window.component';

describe('FoodWindowComponent', () => {
  let component: FoodWindowComponent;
  let fixture: ComponentFixture<FoodWindowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FoodWindowComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FoodWindowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
