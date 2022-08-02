import { JobAdvertManagementComponent } from './../job-advert-management/job-advert-management.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PATHS } from 'src/app/core/constants';
import { MenuListComponent } from './menu-list/menu-list.component';
import { MenuManagementComponent } from './menu-management.component';

const routes: Routes = [
  {
    path: PATHS.empty,
    component: MenuManagementComponent,
    children: [
      {
        path: PATHS.menuList,
        component: MenuListComponent
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MenuManagementRoutingModule { }
