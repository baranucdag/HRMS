import {
  FormGroup,
  FormControl,
  FormBuilder,
  Validators,
} from '@angular/forms';
import { IJobAdvert } from './../../../../core/models/views/jobAdvert.model';
import { JobAdvertService } from './../../../../core/services/api/job-advert.service';
import { Component, EventEmitter, OnInit } from '@angular/core';
import { setSavingStatus } from 'src/app/core/helpers/sidebar';
import { IOnInitializingParam } from 'src/app/core/components/models';
import { Subject, takeUntil } from 'rxjs';
import { SidebarDialogResult, SidebarDialogResultStatus } from 'src/app/core/components/dialogs/sidebar-dialog/enums';
import { IFormComponent } from 'src/app/core/components/interfaces';

@Component({
  selector: 'app-job-advert-create',
  templateUrl: './job-advert-create.component.html',
  styleUrls: ['./job-advert-create.component.scss'],
})
export class JobAdvertCreateComponent implements OnInit,IFormComponent {
  initialData!: {id:number};
  jobAdvertForm!: FormGroup;
  get f(): any {
    return this.jobAdvertForm.controls;
  }

  public readonly onInitializing = new Subject<IOnInitializingParam>();
  public readonly onResult = new EventEmitter<SidebarDialogResult>();
  private readonly onDestroy = new Subject<void>();

  constructor(
    private jobAdvertService: JobAdvertService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.createForm();
  }

  setData(data: any) {
    this.initialData = data;

  }

  createForm() {
    this.jobAdvertForm = this.formBuilder.group({
      positionName: ['', Validators.required],
      qualificationLevel: ['', Validators.required],
      workType: ['', Validators.required],
      publishDate: new Date(),
      deadline: ['', Validators.required],
    });
  }

  save() {
    if (this.jobAdvertForm.invalid) {
      return;
    }

    this.jobAdvertForm.disable();
    setSavingStatus(this.onInitializing, true);
    let sendModel: IJobAdvert = Object.assign({}, this.jobAdvertForm.value);
    return this.jobAdvertService
      .add(sendModel)
      .pipe(takeUntil(this.onDestroy))
      .subscribe(
        (response) => {
          setSavingStatus(this.onInitializing, false);
          this.onResult.emit({ status: SidebarDialogResultStatus.saveSuccess });
        },
        (error) => {
          setSavingStatus(this.onInitializing, false);
          this.onResult.emit({ status: SidebarDialogResultStatus.saveFail });
          this.jobAdvertForm.enable();
        }
      );
  }
}
