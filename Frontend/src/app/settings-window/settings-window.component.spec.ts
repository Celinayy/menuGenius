import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SettingsWindowComponent } from './settings-window.component';

describe('SettingsWindowComponent', () => {
  let component: SettingsWindowComponent;
  let fixture: ComponentFixture<SettingsWindowComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SettingsWindowComponent]
    });
    fixture = TestBed.createComponent(SettingsWindowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
