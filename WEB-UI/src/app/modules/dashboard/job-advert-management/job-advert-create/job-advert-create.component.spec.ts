import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobAdvertCreateComponent } from './job-advert-create.component';

describe('JobAdvertCreateComponent', () => {
  let component: JobAdvertCreateComponent;
  let fixture: ComponentFixture<JobAdvertCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JobAdvertCreateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JobAdvertCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
