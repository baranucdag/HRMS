import { ApplyFormWorkExperienceComponent } from './apply-form/apply-form-work-experience/apply-form-work-experience.component';
import { ApplyFormConfirmationComponent } from './apply-form/apply-form-confirmation/apply-form-confirmation.component';
import { ApplyFormEducationComponent } from './apply-form/apply-form-education/apply-form-education.component';
import { ApplyFormPersonalComponent } from './apply-form/apply-form-personal/apply-form-personal.component';
import { ApplyFormComponent } from './apply-form/apply-form.component';
import { ApplyCvComponent } from './apply-cv/apply-cv.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PATHS } from 'src/app/core/constants';
import { ApplyComponent } from './apply.component';

const routes: Routes = [
  {
    path: PATHS.empty,
    component: ApplyComponent,
    children: [
      {
        path: ':id/' + PATHS.cv,
        component: ApplyCvComponent,
      },
      {
        path: PATHS.form,
        component: ApplyFormComponent,
        children: [
          { path: PATHS.empty, redirectTo:PATHS.personal,pathMatch:'full' },
          { path: PATHS.personal, component: ApplyFormPersonalComponent },
          { path: PATHS.education, component: ApplyFormEducationComponent },
          { path: PATHS.workexperience, component: ApplyFormWorkExperienceComponent },
          { path: PATHS.confirmation, component: ApplyFormConfirmationComponent },
         ],
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ApplyRoutingModule {}
