import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplyFormConfirmationComponent } from './apply-form-confirmation.component';

describe('ApplyFormConfirmationComponent', () => {
  let component: ApplyFormConfirmationComponent;
  let fixture: ComponentFixture<ApplyFormConfirmationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApplyFormConfirmationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ApplyFormConfirmationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
