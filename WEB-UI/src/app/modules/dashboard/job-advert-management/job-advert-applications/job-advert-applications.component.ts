import { MessageService } from 'primeng/api';
import { ApplicationService } from 'src/app/core/services/api';
import { Form, FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { IApplication } from './../../../../core/models/views/application.model';
import { IGridComponent } from 'src/app/core/components/interfaces';
import { Subject, take, takeUntil } from 'rxjs';
import { Component, EventEmitter, OnInit, ViewChild } from '@angular/core';
import { TableComponent } from 'src/app/core/components/tables';
import { ITableOptions } from 'src/app/core/components/tables/table/models';
import { SidebarDialogResult } from 'src/app/core/components/dialogs/sidebar-dialog/enums';
import { enumToArray } from 'src/app/core/helpers/enum';
import { applicationStatusOptions, department } from 'src/app/core/enums';

@Component({
  selector: 'app-job-advert-applications',
  templateUrl: './job-advert-applications.component.html',
  styleUrls: ['./job-advert-applications.component.scss'],
})
export class JobAdvertApplicationsComponent implements OnInit {
  @ViewChild('table') table?: TableComponent;
  baseCvPath: string = 'https://localhost:44313/Uploads/cv/';

  initialData!: { id: number };
  tableOptions!: ITableOptions;
  applications?: any[];
  status: any = 0;

  aapplicationStatusOptions: any = enumToArray(applicationStatusOptions).map(
    (m) => {
      return { label: m.description.toCapitalize(), value: m.id };
    }
  );

  public readonly onResult = new EventEmitter<SidebarDialogResult>();
  private readonly onDestroy = new Subject<void>();

  constructor(
    private applicationService: ApplicationService,
    private formBuilder: FormBuilder,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
  }

  ngOnDestroy() {
    this.onDestroy.next();
    this.onDestroy.complete();
  }

  //set data (id)
  setData(data: any) {
    this.initialData = data;
    if (this.initialData.id) {
      this.getApplicationsByJobAdvertId(this.initialData.id);
    } else {
      // create
    }
  }

  getApplicationsByJobAdvertId(id: number) {
    this.applicationService
      .getByJobAdvertId(id)
      .pipe(takeUntil(this.onDestroy))
      .subscribe(
        (response) => {
          this.applications = response.data;
          
        },
        (resposeError) => {}
      );
  }

  getCandidateCvPath(cvPath: string) {
    if (cvPath !== null) {
      console.log(this.baseCvPath + cvPath);
      return this.baseCvPath + cvPath;
    } else
      return this.messageService.add({
        severity: 'success',
        detail: 'file not exist',
      });
  }

  save(application: any) {
    let applicationToUpdate: IApplication = {
      id: application.id,
      candidateId: application.candidateId,
      applicationStatus: application.applicationStatus,
      prevApplicationStatus: application.applicationStatus - 1,
      applicationDate: application.applicationDate,
      hasEmailSent: application.hasEmailSent,
      isDeleted: application.isDeleted,
      jobAdvertId: application.jobAdvertId,
    };

    this.applicationService
      .update(applicationToUpdate)
      .pipe(takeUntil(this.onDestroy))
      .subscribe((response) => {
        this.messageService.add({
          severity: 'success',
          detail: response.body?.message,
        });
        this.getApplicationsByJobAdvertId(this.initialData.id);
      });
  }
}
