import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrFormConsentComponent } from './hr-form-consent.component';

describe('HrFormConsentComponent', () => {
  let component: HrFormConsentComponent;
  let fixture: ComponentFixture<HrFormConsentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HrFormConsentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HrFormConsentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
