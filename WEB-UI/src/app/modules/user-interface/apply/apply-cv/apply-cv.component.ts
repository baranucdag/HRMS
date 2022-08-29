import { IButtonOptions } from './../../../../core/components/buttons/button/models/button-options.model';
import { CandidateService } from './../../../../core/services/api/candidate.service';
import { AuthService } from './../../../../core/services/api/auth.service';
import { Subject, takeUntil } from 'rxjs';
import {
  ApplicationService,
  JobAdvertService,
} from 'src/app/core/services/api';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MessageService } from 'primeng/api';
import {
  ButtonColor,
  ButtonSize,
} from 'src/app/core/components/buttons/button/enums';

@Component({
  selector: 'app-apply-cv',
  templateUrl: './apply-cv.component.html',
  styleUrls: ['./apply-cv.component.scss'],
})
export class ApplyCvComponent implements OnInit, OnDestroy {
  id!: number;
  application!: any;
  currentCandidate!: any;
  currentUser!: any;
  currentJobAdvert?: any;
  applyButtonOptions!: IButtonOptions;
  showCvInput: boolean = false;

  private readonly onDestroy = new Subject<void>();

  constructor(
    private applicationService: ApplicationService,
    private jobAdvertService: JobAdvertService,
    private candidateService: CandidateService,
    private route: ActivatedRoute,
    private authService: AuthService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.authService.getUserDetailsFromToken();
    this.getParameters();
  }

  ngOnDestroy() {
    this.onDestroy.next();
    this.onDestroy.complete();
  }

  myUploader(event: any) {
    if (this.currentCandidate !== undefined) {
      this.uploadCv(event.files[0], this.currentCandidate);
    }
  }

  getParameters() {
    this.route.params.subscribe((params) => {
      if (
        params !== null &&
        params !== undefined &&
        this.authService.decodedToken !== null &&
        this.authService.decodedToken !== undefined
      ) {
        this.id = Number(params['id']);
        this.getJobAdvertById(this.id);
        this.currentUser = this.authService.decodedToken;
      }
    });
  }

  getJobAdvertById(id: number) {
    this.jobAdvertService
      .getById(id)
      .pipe(takeUntil(this.onDestroy))
      .subscribe((response) => {
        this.currentJobAdvert = response.body?.data;
        this.getCandidatebyUserId(this.currentUser.UserId);
      });
  }

  addApplication() {
    let application: any = {
      candidateId: this.currentCandidate.id,
      jobAdvertId: this.id,
      applicationStaus: 0,
    };
    this.applicationService
      .add(application)
      .pipe(takeUntil(this.onDestroy))
      .subscribe(
        (response) => {
          this.getApplicationById(
            this.currentJobAdvert.id,
            this.currentCandidate.id
          );
          this.messageService.add({
            severity: 'success',
            detail: response.body?.message,
          });
        },
        (responseError) => {
          this.messageService.add({
            severity: 'error',
            detail: responseError,
            data: responseError,
          });
        }
      );
  }

  getCandidatebyUserId(id: number) {
    this.candidateService
      .getByUserId(id)
      .pipe(takeUntil(this.onDestroy))
      .subscribe(
        (response) => {
          this.currentCandidate = response.data;
          this.getApplicationById(
            this.currentJobAdvert.id,
            this.currentCandidate.id
          );
        },
        (responseError) => {}
      );
  }

  checkIfCvExist() {
    if (this.currentCandidate.cvPath) {
      return true;
    } else return false;
  }

  setButtonOptions() {
    this.applyButtonOptions = {
      label: 'Apply',
      hidden:
        this.currentCandidate.cvPath === null || this.application
          ? true
          : false,
      properties: [ButtonSize.normal, ButtonColor.primary],
      class: 'w-100',
    };
  }

  getApplicationById(jobAdvertId: number, candidateId: number) {
    this.applicationService
      .getByUserAndCandidateId(jobAdvertId, candidateId)
      .pipe(takeUntil(this.onDestroy))
      .subscribe((response) => {
        this.application = response.data;
        this.setButtonOptions();
      });
  }

  uploadCv(file: File, candidate: any) {
    console.log(candidate.id);
    const sendForm = new FormData();
    sendForm.append('cv', file);
    sendForm.append('Id', JSON.stringify(candidate.id));
    this.candidateService
      .updateWithCv(sendForm)
      .pipe(takeUntil(this.onDestroy))
      .subscribe(
        (response) => {
          this.messageService.add({
            severity: 'success',
            detail: response.body?.message,
          });
          this.checkIfCvExist();
          this.getCandidatebyUserId(this.currentUser.UserId);
        },
        (responseError) => {
          this.messageService.add({
            severity: 'error',
            detail: responseError.body?.message,
          });
        }
      );
  }
}
