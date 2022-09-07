import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FileUploadModule } from 'primeng/fileupload';
import { CardModule } from 'primeng/card';
import { ApplyRoutingModule } from './apply-routing.module';
import { ApplyComponent } from './apply.component';
import { ApplyCvComponent } from './apply-cv/apply-cv.component';
import { ApplyFormComponent } from './apply-form/apply-form.component';
import { StepsModule } from 'primeng/steps';
import { ApplyFormEducationComponent } from './apply-form/apply-form-education/apply-form-education.component';
import { ApplyFormPersonalComponent } from './apply-form/apply-form-personal/apply-form-personal.component';
import { ApplyFormConfirmationComponent } from './apply-form/apply-form-confirmation/apply-form-confirmation.component';
import { ApplyFormWorkExperienceComponent } from './apply-form/apply-form-work-experience/apply-form-work-experience.component';
import {InputSwitchModule} from 'primeng/inputswitch';

@NgModule({
  declarations: [
    ApplyComponent,
    ApplyCvComponent,
    ApplyFormComponent,
    ApplyFormEducationComponent,
    ApplyFormPersonalComponent,
    ApplyFormConfirmationComponent,
    ApplyFormWorkExperienceComponent,
  ],
  imports: [
    CommonModule,
    ApplyRoutingModule,
    InputSwitchModule,
    SharedModule,
    FileUploadModule,
    CardModule,
    StepsModule,
  ],
})
export class ApplyModule {}
