import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { AuthComponent } from './auth.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PATHS } from 'src/app/core/constants';

const routes: Routes = [
  {
    path: PATHS.empty,
    component: AuthComponent,
    children: [
      {
        path: PATHS.login,
        component: LoginComponent,
      },
      {
        path: PATHS.register,
        component: RegisterComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AuthRoutingModule {}
