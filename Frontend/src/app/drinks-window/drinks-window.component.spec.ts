import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DrinksWindowComponent } from './drinks-window.component';

describe('DrinksWindowComponent', () => {
  let component: DrinksWindowComponent;
  let fixture: ComponentFixture<DrinksWindowComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DrinksWindowComponent]
    });
    fixture = TestBed.createComponent(DrinksWindowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
