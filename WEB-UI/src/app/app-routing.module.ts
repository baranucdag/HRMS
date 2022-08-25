import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PATHS } from './core/constants';

const routes: Routes = [
  {
    path: PATHS.admin,
    loadChildren: () =>
      import('./modules/dashboard/dashboard.module').then(
        (m) => m.DashboardModule
      ),
  },
  /*{
    path: '**',
    redirectTo: PATHS.empty,
    pathMatch: 'full',
  },*/
  {
    path: PATHS.empty,
    loadChildren: () =>
      import('./modules/user-interface/user-interface.module').then(
        (m) => m.UserInterfaceModule
      ),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
