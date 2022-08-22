import { ApplyComponent } from './apply.component';
import { ApplyCvComponent } from './apply-cv/apply-cv.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PATHS } from 'src/app/core/constants';

const routes: Routes = [
  {
    path: PATHS.empty,
    component: ApplyComponent,
    children: [
      {
        path: 'cv',
        component: ApplyCvComponent,
      },
    ],
  },
  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ApplyRoutingModule {}
