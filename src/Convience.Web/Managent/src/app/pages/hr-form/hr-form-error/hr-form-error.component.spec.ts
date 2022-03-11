import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrFormErrorComponent } from './hr-form-error.component';

describe('HrFormErrorComponent', () => {
  let component: HrFormErrorComponent;
  let fixture: ComponentFixture<HrFormErrorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HrFormErrorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HrFormErrorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
