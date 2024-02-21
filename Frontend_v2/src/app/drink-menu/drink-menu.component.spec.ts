import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DrinkMenuComponent } from './drink-menu.component';

describe('DrinkMenuComponent', () => {
  let component: DrinkMenuComponent;
  let fixture: ComponentFixture<DrinkMenuComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DrinkMenuComponent]
    });
    fixture = TestBed.createComponent(DrinkMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
