import { LocalStorageService } from './../../../../../core/services/local-storage/local-storage.service';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { CandidateFormService } from './../../../../../core/services/api/candidate-form.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-apply-form-work-experience',
  templateUrl: './apply-form-work-experience.component.html',
  styleUrls: ['./apply-form-work-experience.component.scss'],
})
export class ApplyFormWorkExperienceComponent implements OnInit {
  workExperienceInformationForm!: FormGroup;
  savedInformations:any
  constructor(
    private formBuilder: FormBuilder,
    private candidateFormService: CandidateFormService,
    private messageService: MessageService,
    private router: Router,
    private localStorageService:LocalStorageService
  ) {}

  ngOnInit(): void {
    this.savedInformations = JSON.parse(this.localStorageService.get('workExperienceInformations')!)
    this.createForm();
  }

  senFormToService() {
    if (this.workExperienceInformationForm.valid) {
      let form: any = Object.assign(
        {},
        this.workExperienceInformationForm.value
      );
      this.candidateFormService.setWorkExperienceInformation(form);
      this.localStorageService.set("workExperienceInformations",JSON.stringify(form))
      this.router.navigate(['/apply/form/confirmation']);
    } else
      this.messageService.add({
        severity: 'error',
        detail: 'form is invalid !',
      });
  }

  createForm() {
    this.workExperienceInformationForm = this.formBuilder.group({
      lastPositionName: [this.savedInformations?this.savedInformations.lastPositionName:'', Validators.required],
      lastPositionDepartment: [this.savedInformations?this.savedInformations.lastPositionDepartment:'', Validators.required],
      lastPositionDescription: [this.savedInformations?this.savedInformations.lastPositionDescription:'', Validators.required],
      lastPositionManagerTitle: [this.savedInformations?this.savedInformations.lastPositionManagerTitle:'', Validators.required],
      lastPositionAdditionalBenefits: [this.savedInformations?this.savedInformations.lastPositionAdditionalBenefits:'', Validators.required],
      possibleStartDate: [this.savedInformations?this.savedInformations.possibleStartDate:'', Validators.required],
      expectedSalary: [this.savedInformations?this.savedInformations.expectedSalary:'', Validators.required],
    });
  }
}
