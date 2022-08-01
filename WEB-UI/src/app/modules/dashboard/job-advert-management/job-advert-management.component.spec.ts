import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobAdvertManagementComponent } from './job-advert-management.component';

describe('JobAdvertManagementComponent', () => {
  let component: JobAdvertManagementComponent;
  let fixture: ComponentFixture<JobAdvertManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JobAdvertManagementComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JobAdvertManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
