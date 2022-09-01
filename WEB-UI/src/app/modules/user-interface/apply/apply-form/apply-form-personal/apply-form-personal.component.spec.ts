import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplyFormPersonalComponent } from './apply-form-personal.component';

describe('ApplyFormPersonalComponent', () => {
  let component: ApplyFormPersonalComponent;
  let fixture: ComponentFixture<ApplyFormPersonalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApplyFormPersonalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ApplyFormPersonalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
