import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthMenuWindowComponent } from './auth-menu-window.component';

describe('AuthMenuWindowComponent', () => {
  let component: AuthMenuWindowComponent;
  let fixture: ComponentFixture<AuthMenuWindowComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AuthMenuWindowComponent]
    });
    fixture = TestBed.createComponent(AuthMenuWindowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
