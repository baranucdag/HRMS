import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CandidateFormService {
  personalInformation: any;
  educationInformation: any;
  workExperinceInformation: any;

  allCandidateInformation: any;
  constructor() {}

  setPersonalInformation(form: any) {
    this.personalInformation = form;
  }

  setEducationInformation(form: any) {
    this.educationInformation = form;
  }

  setWorkExperienceInformation(form: any) {
    this.workExperinceInformation = form;
    this.getAllInformation()
  }

  getAllInformation() {
    this.allCandidateInformation = {
      personal: this.personalInformation,
      education: this.educationInformation,
      workExperience: this.workExperinceInformation,
    };

    return this.allCandidateInformation;
  }
}
