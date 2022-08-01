import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PATHS } from 'src/app/core/constants';
import { DashboardComponent } from './dashboard.component';

const routes: Routes = [
  {
    path: PATHS.empty,
    component: DashboardComponent,
    children:[
      {
        path:PATHS.menuManagement,
        loadChildren:() =>
        import('./menu-management/menu-management.module').then(
          (m) => m.MenuManagementModule
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
