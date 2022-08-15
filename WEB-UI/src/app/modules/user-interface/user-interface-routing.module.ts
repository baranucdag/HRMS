import { HomeComponent } from './../user-interface/home/home.component';
import { UserInterfaceComponent } from './user-interface.component';
import { NgModule } from '@angular/core';
import { PATHS } from 'src/app/core/constants';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: PATHS.empty,
    component: UserInterfaceComponent,
    children: [{ path: PATHS.empty, component: HomeComponent }],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UserInterfaceRoutingModule {}
