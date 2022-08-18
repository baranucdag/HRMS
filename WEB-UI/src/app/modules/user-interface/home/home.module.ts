import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { JobadvertsComponent } from './jobadverts/jobadverts.component';
import { PaginatorModule } from 'primeng/paginator';

@NgModule({
  declarations: [JobadvertsComponent],
  imports: [CommonModule, HomeRoutingModule, SharedModule, PaginatorModule],
  exports: [PaginatorModule],
})
export class HomeModule {}
