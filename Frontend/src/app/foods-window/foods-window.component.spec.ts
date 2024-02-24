import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FoodsWindowComponent } from './foods-window.component';

describe('FoodsWindowComponent', () => {
  let component: FoodsWindowComponent;
  let fixture: ComponentFixture<FoodsWindowComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FoodsWindowComponent]
    });
    fixture = TestBed.createComponent(FoodsWindowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
