import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApplicationManagementRoutingModule } from './application-management-routing.module';
import { ApplicationManagementComponent } from './application-management.component';
import { ApplicationListComponent } from './application-list/application-list.component';
import { SharedModule } from '../..';


@NgModule({
  declarations: [
    ApplicationManagementComponent,
    ApplicationListComponent
  ],
  imports: [
    CommonModule,
    ApplicationManagementRoutingModule,
    SharedModule
  ]
})
export class ApplicationManagementModule { }
