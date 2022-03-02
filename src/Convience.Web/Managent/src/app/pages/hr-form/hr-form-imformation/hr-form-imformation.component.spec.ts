import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrFormImformationComponent } from './hr-form-imformation.component';

describe('HrFormImformationComponent', () => {
  let component: HrFormImformationComponent;
  let fixture: ComponentFixture<HrFormImformationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HrFormImformationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HrFormImformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
