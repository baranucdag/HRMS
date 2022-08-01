import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobAdvertListComponent } from './job-advert-list.component';

describe('JobAdvertListComponent', () => {
  let component: JobAdvertListComponent;
  let fixture: ComponentFixture<JobAdvertListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JobAdvertListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JobAdvertListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
