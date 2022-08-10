import { UserListComponent } from './user-list/user-list.component';
import { MenuManagementComponent } from './../menu-management/menu-management.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PATHS } from 'src/app/core/constants';

const routes: Routes = [
  {
    path: PATHS.empty,
    component: MenuManagementComponent,
    children: [
      {
        path: PATHS.userList,
        component:UserListComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserManagementRoutingModule { }
