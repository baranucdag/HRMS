import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobAdvertApplicationsComponent } from './job-advert-applications.component';

describe('JobAdvertApplicationsComponent', () => {
  let component: JobAdvertApplicationsComponent;
  let fixture: ComponentFixture<JobAdvertApplicationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JobAdvertApplicationsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JobAdvertApplicationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
