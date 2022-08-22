import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobadvertsComponent } from './jobadverts.component';

describe('JobadvertsComponent', () => {
  let component: JobadvertsComponent;
  let fixture: ComponentFixture<JobadvertsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JobadvertsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JobadvertsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
