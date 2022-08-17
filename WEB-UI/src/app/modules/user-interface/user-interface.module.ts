import { FooterComponent } from './../../core/components/user-interface/footer/footer.component';
import { NavigationComponent } from './../../core/components/user-interface/navigation/navigation.component';
import { SharedModule } from './../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserInterfaceRoutingModule } from './user-interface-routing.module';
import { UserInterfaceComponent } from './user-interface.component';
import { HomeComponent } from './home/home.component';


@NgModule({
  declarations: [
    UserInterfaceComponent,
    HomeComponent,
    NavigationComponent,
    FooterComponent
  ],
  imports: [
    CommonModule,
    UserInterfaceRoutingModule,
    SharedModule
  ]
})
export class UserInterfaceModule { }
