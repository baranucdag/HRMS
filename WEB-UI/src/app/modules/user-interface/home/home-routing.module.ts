import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PATHS } from 'src/app/core/constants';
import { JobadvertsComponent } from './jobadverts/jobadverts.component';

const routes: Routes = [
  {
    path: PATHS.empty,
    component: JobadvertsComponent,
    children: [
      {
        path: PATHS.empty,
        component: JobadvertsComponent
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
