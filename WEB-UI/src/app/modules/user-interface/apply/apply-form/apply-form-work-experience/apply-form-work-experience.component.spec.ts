import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplyFormWorkExperienceComponent } from './apply-form-work-experience.component';

describe('ApplyFormWorkExperienceComponent', () => {
  let component: ApplyFormWorkExperienceComponent;
  let fixture: ComponentFixture<ApplyFormWorkExperienceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApplyFormWorkExperienceComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ApplyFormWorkExperienceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
