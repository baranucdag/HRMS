import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { JobAdvertManagementRoutingModule } from './job-advert-management-routing.module';
import { JobAdvertManagementComponent } from './job-advert-management.component';
import { JobAdvertCreateComponent } from './job-advert-create/job-advert-create.component';
import { JobAdvertListComponent } from './job-advert-list/job-advert-list.component';


@NgModule({
  declarations: [
    JobAdvertManagementComponent,
    JobAdvertCreateComponent,
    JobAdvertListComponent
  ],
  imports: [
    SharedModule,
    CommonModule,
    JobAdvertManagementRoutingModule
  ]
})
export class JobAdvertManagementModule { }
