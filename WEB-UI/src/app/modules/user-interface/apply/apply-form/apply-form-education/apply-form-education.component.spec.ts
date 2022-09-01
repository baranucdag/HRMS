import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplyFormEducationComponent } from './apply-form-education.component';

describe('ApplyFormEducationComponent', () => {
  let component: ApplyFormEducationComponent;
  let fixture: ComponentFixture<ApplyFormEducationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApplyFormEducationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ApplyFormEducationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
