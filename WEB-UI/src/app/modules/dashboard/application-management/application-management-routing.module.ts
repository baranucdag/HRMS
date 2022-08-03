import { ApplicationListComponent } from './application-list/application-list.component';
import { ApplicationManagementComponent } from './application-management.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PATHS } from 'src/app/core/constants';

const routes: Routes = [
  {
    path: PATHS.empty,
    component: ApplicationManagementComponent,
    children: [
      {
        path: PATHS.applicationList,
        component: ApplicationListComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ApplicationManagementRoutingModule {}
