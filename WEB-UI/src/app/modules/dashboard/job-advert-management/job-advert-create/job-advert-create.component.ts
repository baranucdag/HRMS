import {
  workPlaceTypeEnum,
  workTimeType,
} from './../../../../core/enums/dropdown-select-options';
import { IDropdownOptions } from './../../../../core/components/dropdowns/dropdown/models/dropdown-options.model';
import {
  FormGroup,
  FormControl,
  FormBuilder,
  Validators,
} from '@angular/forms';
import { IJobAdvert } from './../../../../core/models/views/jobAdvert.model';
import { JobAdvertService } from './../../../../core/services/api/job-advert.service';
import { Component, EventEmitter, OnInit } from '@angular/core';
import {
  setSavingStatus,
  setUpdatingStatus,
} from 'src/app/core/helpers/sidebar';
import { IOnInitializingParam } from 'src/app/core/components/models';
import { pipe, Subject, takeUntil } from 'rxjs';
import {
  SidebarDialogResult,
  SidebarDialogResultStatus,
} from 'src/app/core/components/dialogs/sidebar-dialog/enums';
import { IFormComponent } from 'src/app/core/components/interfaces';
import { dateTransformForBackend } from 'src/app/core/helpers/date/dateTransform';
import { DatePipe } from '@angular/common';
import { enumToArray } from 'src/app/core/helpers/enum';

@Component({
  selector: 'app-job-advert-create',
  templateUrl: './job-advert-create.component.html',
  styleUrls: ['./job-advert-create.component.scss'],
})
export class JobAdvertCreateComponent implements OnInit, IFormComponent {
  initialData!: { id: number };
  jobAdvertForm!: FormGroup;
  selectedJobAdvert?: any;
  // workPlaceTypes:string[]=['Remote','From Office','Hybrit']
  //workTimeTypes:string[]=['Full Time','half']
  selectedWorkPlaceType: any;
  selectedWorkTimeType: any;
  workPlaceTypeDropdownOptions?: IDropdownOptions;
  workTimeTypeDropdownOptions?: IDropdownOptions;

  workTimeTypes: any = enumToArray(workTimeType).map((m) => {
    return { label: m.description.toCapitalize(), value: m.id };
  });

  workPlaceTypes: any = enumToArray(workPlaceTypeEnum).map((m) => {
    return { label: m.description.toCapitalize(), value: m.id };
  });

  get f(): any {
    return this.jobAdvertForm.controls;
  }

  public readonly onInitializing = new Subject<IOnInitializingParam>();
  public readonly onResult = new EventEmitter<SidebarDialogResult>();
  private readonly onDestroy = new Subject<void>();

  constructor(
    private jobAdvertService: JobAdvertService,
    private formBuilder: FormBuilder,
    private datePipe: DatePipe
  ) {}

  ngOnInit(): void {
    this.createForm();
  }

  setData(data: any) {
    this.initialData = data;
    if (this.initialData.id) {
      // update
      this.getJobAdvert(this.initialData.id);
    } else {
      // create
    }
  }

  getJobAdvert(id: number) {
    this.jobAdvertService
      .getById(id)
      .pipe(takeUntil(this.onDestroy))
      .subscribe((response: any) => {
        const jobAdvert = response.body.data;
        this.selectedJobAdvert = jobAdvert;
        this.createForm(jobAdvert);
      });
  }

  setWorkPlaceTypeDropdownOptions(data: any, selected?: any) {
    // if (selected == undefined) {
    //   selected = data[0];
    //   this.selectedWorkPlaceType = selected;
    // }
    this.workPlaceTypeDropdownOptions = {
      items: data,
      onSelectionChange: (value) => {
        this.selectedWorkPlaceType = value;
      },
      optionLabel: 'label',
      placeholder: 'Select',
      selected: selected,
    };
    this.workPlaceTypeDropdownOptions.errors?.next([]);
  }

  setWorkTimeTypeDropdownOptions(data: any, selected?: any) {
    // if (selected == undefined) {
    //   selected = data[0];
    //   this.selectedWorkPlaceType = selected;
    // }
    this.workTimeTypeDropdownOptions = {
      items: data,
      onSelectionChange: (value) => {
        this.selectedWorkTimeType = value;
      },
      optionLabel: 'label',
      placeholder: 'Select',
      selected: selected,
    };
    this.workTimeTypeDropdownOptions.errors?.next([]);
  }

  createForm(jobAdvert?: IJobAdvert) {
    //gelen deadline verisinin uygun formata dönüştürülmesi
    let deadLine = this.datePipe.transform(jobAdvert?.deadline, 'yyyy-MM-dd');

    this.jobAdvertForm = this.formBuilder.group({
      positionName: [jobAdvert?.positionName, Validators.required],
      qualificationLevel: [jobAdvert?.qualificationLevel, Validators.required],
      publishDate: new Date(),
      description: [jobAdvert?.description, Validators.required],
      deadline: [deadLine, Validators.required],
    });

    let selectedWorkPlaceType;
    let selectedWorkTimeType;
    if (this.selectedJobAdvert) {
      selectedWorkTimeType = this.workTimeTypes.find(
        (x: any) => x.label === this.selectedJobAdvert.workTimeType
      );
      selectedWorkPlaceType = this.workPlaceTypes.find(
        (x: any) => x.label === this.selectedJobAdvert.workPlaceType
      );
    }
    this.setWorkTimeTypeDropdownOptions(
      this.workTimeTypes,
      selectedWorkTimeType
    );
    this.setWorkPlaceTypeDropdownOptions(
      this.workPlaceTypes,
      selectedWorkPlaceType
    );
  }

  save() {
    Object.keys(this.jobAdvertForm.controls).forEach((key) => {
      this.jobAdvertForm.get(key)?.markAsDirty();
    });

    if (this.jobAdvertForm.invalid) {
      return;
    }

    this.jobAdvertForm.disable();
    setSavingStatus(this.onInitializing, true);
    let sendModel: IJobAdvert = Object.assign({}, this.jobAdvertForm.value);

    sendModel.deadline = dateTransformForBackend(sendModel.deadline);
    sendModel.workPlaceType = this.selectedWorkPlaceType.label;
    sendModel.workTimeType = this.selectedWorkTimeType.label;

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
    Object.keys(this.jobAdvertForm.controls).forEach((key) => {
      this.jobAdvertForm.get(key)?.markAsDirty();
    });

    if (this.jobAdvertForm.invalid) {
      return;
    }

    this.jobAdvertForm.disable();
    setUpdatingStatus(this.onInitializing, true);

    let jobAdvertModel: IJobAdvert = Object.assign(
      this.selectedJobAdvert,
      this.jobAdvertForm.value
    );
    if (this.selectedWorkPlaceType != undefined) {
      jobAdvertModel.workPlaceType = this.selectedWorkPlaceType.label;
    }
    if (this.selectedWorkTimeType != undefined) {
      jobAdvertModel.workTimeType = this.selectedWorkTimeType?.label;
    }

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
