import { TestBed } from '@angular/core/testing';

import { HrUserService } from './hr-user.service';

describe('HrUserService', () => {
  let service: HrUserService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HrUserService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
