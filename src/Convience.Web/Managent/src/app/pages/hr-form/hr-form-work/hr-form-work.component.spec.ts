import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HrFormWorkComponent } from './hr-form-work.component';

describe('HrFormWorkComponent', () => {
  let component: HrFormWorkComponent;
  let fixture: ComponentFixture<HrFormWorkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HrFormWorkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HrFormWorkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
