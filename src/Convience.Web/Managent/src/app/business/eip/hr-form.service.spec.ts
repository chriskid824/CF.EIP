import { TestBed } from '@angular/core/testing';

import { HrFormService } from './hr-form.service';

describe('HrFormService', () => {
  let service: HrFormService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HrFormService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
