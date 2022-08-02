import { JobAdvertListComponent } from './job-advert-list/job-advert-list.component';
import { JobAdvertManagementComponent } from './job-advert-management.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PATHS } from 'src/app/core/constants';

const routes: Routes = [
  {
    path: PATHS.empty,
    component: JobAdvertManagementComponent,
    children: [
      {
        path: PATHS.jobAdvertList,
        component: JobAdvertListComponent
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class JobAdvertManagementRoutingModule { }
