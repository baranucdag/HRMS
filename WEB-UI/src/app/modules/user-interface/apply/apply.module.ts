import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FileUploadModule } from 'primeng/fileupload';
import {CardModule} from 'primeng/card';
import { ApplyRoutingModule } from './apply-routing.module';
import { ApplyComponent } from './apply.component';
import { ApplyCvComponent } from './apply-cv/apply-cv.component';

@NgModule({
  declarations: [ApplyComponent, ApplyCvComponent],
  imports: [CommonModule, ApplyRoutingModule, SharedModule, FileUploadModule,CardModule],
})
export class ApplyModule {}
