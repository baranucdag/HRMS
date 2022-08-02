import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MenuManagementRoutingModule } from './menu-management-routing.module';
import { MenuManagementComponent } from './menu-management.component';
import { MenuListComponent } from './menu-list/menu-list.component';
import { SharedModule } from '../../shared/shared.module';
import { MenuCreateComponent } from './menu-create/menu-create.component';




@NgModule({
  declarations: [
    MenuManagementComponent,
    MenuListComponent,
    MenuCreateComponent,
  ],
  imports: [
    CommonModule,
    MenuManagementRoutingModule,
    SharedModule
  ]
})
export class MenuManagementModule { }
