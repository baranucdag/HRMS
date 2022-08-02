import { TestBed } from '@angular/core/testing';

import { JobAdvertService } from './job-advert.service';

describe('JobAdvertService', () => {
  let service: JobAdvertService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(JobAdvertService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
