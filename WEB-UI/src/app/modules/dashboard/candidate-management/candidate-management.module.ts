import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CandidateManagementRoutingModule } from './candidate-management-routing.module';
import { CandidateManagementComponent } from './candidate-management.component';
import { CandidateListComponent } from './candidate-list/candidate-list.component';


@NgModule({
  declarations: [
    CandidateManagementComponent,
    CandidateListComponent
  ],
  imports: [
    CommonModule,
    CandidateManagementRoutingModule,
    SharedModule
  ]
})
export class CandidateManagementModule { }
