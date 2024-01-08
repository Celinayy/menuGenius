import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginWindowComponent } from './login-window.component';

describe('LoginWindowComponent', () => {
  let component: LoginWindowComponent;
  let fixture: ComponentFixture<LoginWindowComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LoginWindowComponent]
    });
    fixture = TestBed.createComponent(LoginWindowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
