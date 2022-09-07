import { TestBed } from '@angular/core/testing';

import { ApplicationFormGuard } from './application-form.guard';

describe('ApplicationFormGuard', () => {
  let guard: ApplicationFormGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(ApplicationFormGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
