import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { TranslateModule } from '@ngx-translate/core';
import { DropdownComponent } from 'src/app/core/components/dropdowns';
import { DropdownModule } from 'primeng/dropdown';
import { ButtonModule } from 'primeng/button';
import { ButtonComponent } from 'src/app/core/components/buttons';
import { TooltipModule } from 'primeng/tooltip';
import { CustomDatePipe, SafeHtmlPipe } from 'src/app/core/pipes';
import { StringTemplatePipe } from 'src/app/core/components/tables/table/pipes';
import { CheckboxModule } from 'primeng/checkbox';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { InputSwitchModule } from 'primeng/inputswitch';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { TableComponent } from 'src/app/core/components/tables';
import { SidebarComponent } from 'src/app/core/components/navigation';
import { AccordionModule } from 'primeng/accordion';
import { SkeletonModule } from 'primeng/skeleton';
import { RouterModule } from '@angular/router';
import {
  DialogComponent,
  SidebarDialogComponent,
} from 'src/app/core/components/dialogs';
import { ToolbarComponent } from 'src/app/core/components/toolbars';
import { SidebarModule } from 'primeng/sidebar';
import { ToolbarModule } from 'primeng/toolbar';
import { DialogModule } from 'primeng/dialog';
import { PanelModule } from 'primeng/panel';
import { InsertionDirective } from 'src/app/core/directives';
import { HeaderComponent } from 'src/app/core/components/navigation/header/header.component';
import { PanelMenuModule } from 'primeng/panelmenu';
import { MenubarModule } from 'primeng/menubar';
import { Calendar, CalendarModule } from 'primeng/calendar';
import '../../core/extensions';

@NgModule({
  declarations: [
    DropdownComponent,
    ButtonComponent,
    TableComponent,
    CustomDatePipe,
    SafeHtmlPipe,
    StringTemplatePipe,
    SidebarComponent,
    SidebarDialogComponent,
    ToolbarComponent,
    DialogComponent,
    InsertionDirective,
    HeaderComponent,
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    TranslateModule,
    DropdownModule,
    TooltipModule,
    ButtonModule,
    CheckboxModule,
    TableModule,
    ToastModule,
    InputSwitchModule,
    InputNumberModule,
    InputTextModule,
    AccordionModule,
    SkeletonModule,
    RouterModule,
    SidebarModule,
    ToolbarModule,
    DialogModule,
    PanelModule,
    PanelMenuModule,
    MenubarModule,
  ],
  exports: [
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    ButtonComponent,
    DropdownComponent,
    TranslateModule,
    TooltipModule,
    AccordionModule,
    ButtonModule,
    CustomDatePipe,
    SafeHtmlPipe,
    StringTemplatePipe,
    CheckboxModule,
    TableModule,
    ToastModule,
    TableComponent,
    InputSwitchModule,
    InputNumberModule,
    InputTextModule,
    SkeletonModule,
    SidebarComponent,
    RouterModule,
    ToolbarComponent,
    MenubarModule,
    ToolbarModule,
    SidebarModule,
    DialogComponent,
    DialogModule,
    PanelModule,
    PanelMenuModule,
    HeaderComponent,
    CalendarModule,
  ],
  providers: [DatePipe, StringTemplatePipe, SafeHtmlPipe],
  entryComponents: [SidebarDialogComponent],
})
export class SharedModule {}
