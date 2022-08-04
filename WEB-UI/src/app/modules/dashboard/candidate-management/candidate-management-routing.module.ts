import { CandidateListComponent } from './candidate-list/candidate-list.component';
import { CandidateManagementComponent } from './candidate-management.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PATHS } from 'src/app/core/constants';

const routes: Routes = [
  {
    path: PATHS.empty,
    component: CandidateManagementComponent,
    children: [
      {
        path: PATHS.candidateList,
        component:CandidateListComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CandidateManagementRoutingModule {}
