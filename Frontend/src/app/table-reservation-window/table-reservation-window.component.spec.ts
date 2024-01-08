import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TableReservationWindowComponent } from './table-reservation-window.component';

describe('TableReservationWindowComponent', () => {
  let component: TableReservationWindowComponent;
  let fixture: ComponentFixture<TableReservationWindowComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TableReservationWindowComponent]
    });
    fixture = TestBed.createComponent(TableReservationWindowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
