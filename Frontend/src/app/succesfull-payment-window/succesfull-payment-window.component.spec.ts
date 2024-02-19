import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SuccesfullPaymentWindowComponent } from './succesfull-payment-window.component';

describe('SuccesfullPaymentWindowComponent', () => {
  let component: SuccesfullPaymentWindowComponent;
  let fixture: ComponentFixture<SuccesfullPaymentWindowComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SuccesfullPaymentWindowComponent]
    });
    fixture = TestBed.createComponent(SuccesfullPaymentWindowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
