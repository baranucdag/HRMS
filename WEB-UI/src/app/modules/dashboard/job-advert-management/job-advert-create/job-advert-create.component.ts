import {
  FormGroup,
  FormControl,
  FormBuilder,
  Validators,
} from '@angular/forms';
import { IJobAdvert } from './../../../../core/models/views/jobAdvert.model';
import { JobAdvertService } from './../../../../core/services/api/job-advert.service';
import { Component, EventEmitter, OnInit } from '@angular/core';
import { setSavingStatus, setUpdatingStatus } from 'src/app/core/helpers/sidebar';
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

  getJobAdvert(id: number) {
    this.jobAdvertService
      .getById(id)
      .pipe(takeUntil(this.onDestroy))
      .subscribe((response: any) => {
        const menu = response.body.data;
        this.createForm(menu);
      });
  }

  createForm(jobAdvert?:IJobAdvert) {
    this.jobAdvertForm = this.formBuilder.group({
      positionName: [jobAdvert?.positionName, Validators.required],
      qualificationLevel: [jobAdvert?.qualificationLevel, Validators.required],
      workType: [jobAdvert?.workType, Validators.required],
      publishDate: new Date(),
      description: [jobAdvert?.description, Validators.required],
      deadline: [jobAdvert?.deadline, Validators.required],
    });
  }
  //todo: object.keys ne işe yarıyor burada
  save() {

    // Object.keys(this.jobAdvertForm.controls).forEach((key) => {
    //   this.jobAdvertForm.get(key)?.markAsDirty();
    // });

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

  
  update() {
    // Object.keys(this.jobAdvertForm.controls).forEach((key) => {
    //   this.jobAdvertForm.get(key)?.markAsDirty();
    // });

    if (this.jobAdvertForm.invalid) {
      return;
    }

    this.jobAdvertForm.disable();
    setUpdatingStatus(this.onInitializing, true);

   let jobAdvertModel:IJobAdvert = Object.assign({},this.jobAdvertForm.value)

    if (this.initialData.id) {
      this.jobAdvertService
        .update(jobAdvertModel)
        .pipe(takeUntil(this.onDestroy))
        .subscribe(
          (response) => {
            this.onResult.emit({
              status: SidebarDialogResultStatus.updateSuccess,
            });
            setUpdatingStatus(this.onInitializing, false);
          },
          (error) => {
            this.onResult.emit({
              status: SidebarDialogResultStatus.updateFail,
            });
            this.jobAdvertForm.enable();
            setUpdatingStatus(this.onInitializing, false);
          }
        );
    }
  }
}
