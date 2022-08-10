import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PATHS } from 'src/app/core/constants';
import { DashboardComponent } from './dashboard.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {
    path: PATHS.empty,
    component: DashboardComponent,
    children:[
      {
        path:PATHS.empty,
        component:HomeComponent
      },
      {
        path:PATHS.menuManagement,
        loadChildren:() =>
        import('./menu-management/menu-management.module').then(
          (m) => m.MenuManagementModule
        ),
      },
      {
        path:PATHS.jobAdvert,
        loadChildren:() =>
        import('./job-advert-management/job-advert-management.module').then(
          (m) => m.JobAdvertManagementModule
        ),
      },
      {
        path:PATHS.candidate,
        loadChildren:() =>
        import('./candidate-management/candidate-management.module').then(
          (m) => m.CandidateManagementModule
        ),
      },
      {
        path:PATHS.application,
        loadChildren:() =>
        import('./application-management/application-management.module').then(
          (m) => m.ApplicationManagementModule
        ),
      },
      {
        path:PATHS.user,
        loadChildren:() =>
        import('./user-management/user-management.module').then(
          (m) => m.UserManagementModule
        ),
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
