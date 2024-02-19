import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CancelPaymentWindowComponent } from './cancel-payment-window.component';

describe('CancelPaymentWindowComponent', () => {
  let component: CancelPaymentWindowComponent;
  let fixture: ComponentFixture<CancelPaymentWindowComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CancelPaymentWindowComponent]
    });
    fixture = TestBed.createComponent(CancelPaymentWindowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
