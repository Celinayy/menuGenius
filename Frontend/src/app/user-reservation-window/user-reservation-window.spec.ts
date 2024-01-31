import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HistoryWindowComponent } from './user-reservation-window.component';

describe('HistoryWindowComponent', () => {
  let component: HistoryWindowComponent;
  let fixture: ComponentFixture<HistoryWindowComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HistoryWindowComponent]
    });
    fixture = TestBed.createComponent(HistoryWindowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
