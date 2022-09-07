import { Subject, takeUntil } from 'rxjs';
import { AuthService } from 'src/app/core/services/api/auth.service';
import { CandidateService } from './../../../../../core/services/api/candidate.service';
import { LocalStorageService } from './../../../../../core/services/local-storage/local-storage.service';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import { CandidateFormService } from './../../../../../core/services/api/candidate-form.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-apply-form-education',
  templateUrl: './apply-form-education.component.html',
  styleUrls: ['./apply-form-education.component.scss'],
})
export class ApplyFormEducationComponent implements OnInit {
  educationInformationForm!: FormGroup;
  savedInformations:any
  currentCandidate:any
  candidateLocalStorage:any

  private readonly onDestroy = new Subject<void>();

  constructor(
    private formBuilder: FormBuilder,
    private candidateFormService: CandidateFormService,
    private router: Router,
    private messageService: MessageService,
    private localStorageService:LocalStorageService,
    private candidateService:CandidateService,
    private authService:AuthService
  ) {}

  ngOnInit(): void { 
    this.getCandidateByUserId()
  }

  ngOnDestrory(){
    this.onDestroy.next()
    this.onDestroy.complete()
  }

  sendDataToService() {
    if (this.educationInformationForm.valid) {
      const form = Object.assign({}, this.educationInformationForm.value);
      this.candidateFormService.setEducationInformation(form);
      this.localStorageService.set("educationInformations",JSON.stringify(form))
      this.router.navigate(['/apply/form/workexperience']);
    } else
      this.messageService.add({
        severity: 'error',
        detail: 'form is invalid !',
      });
  }

  createForm() {
    console.log(this.savedInformations.primarySchoolStartDate);
      this.educationInformationForm = this.formBuilder.group({
      primarySchoolStartDate: [this.savedInformations.primarySchoolStartDate ?? '', Validators.required],
      primarySchoolEndDate: [this.savedInformations.primarySchoolEndDate ?? '', Validators.required],
      middleSchoolStartDate: [this.savedInformations.middleSchoolStartDate??'', Validators.required],
      middleSchoolEndDate: [this.savedInformations.middleSchoolEndDate??'', Validators.required],
      highSchoolStartDate: this.savedInformations.highSchoolStartDate??['', Validators.required],
      highSchoolEndDate: [this.savedInformations.highSchoolEndDate??'', Validators.required],
      universityStartDate: [this.savedInformations.universityStartDate??'', Validators.required],
      universityEndDate: [this.savedInformations.universityEndDate??'', Validators.required],
      masterStartDate: [this.savedInformations.masterStartDate??''],
      masterEndDate: [this.savedInformations.masterEndDate??''],

      englishReadingLevel: [this.savedInformations.englishReadingLevel ?? '', Validators.required],
      englishWritingLevel: [this.savedInformations.englishWritingLevel??'', Validators.required],
      englishSpeakingLevel: [this.savedInformations.englishSpeakingLevel??'', Validators.required],
      frenchReadingLevel: [this.savedInformations.frenchReadingLevel??'', Validators.required],
      frenchWritingLevel: [this.savedInformations.frenchWritingLevel??'', Validators.required],
      frenchSpeakingLevel: [this.savedInformations.frenchSpeakingLevel??'', Validators.required],
      deutchReadingLevel: [this.savedInformations.deutchReadingLevel??'', Validators.required],
      deutchWritingLevel: [this.savedInformations.deutchWritingLevel ?? '', Validators.required],
      deutchSpeakingLevel: [this.savedInformations.deutchSpeakingLevel ?? '', Validators.required],
    });
  }

  getCandidateByUserId(){
    this.authService.getUserDetailsFromToken()
    this.candidateService.getByUserId(this.authService.decodedToken.UserId).pipe(takeUntil(this.onDestroy)).subscribe((response)=>{
      this.currentCandidate = response.data
      this.candidateLocalStorage = JSON.parse(this.localStorageService.get('educationInformations')!)
      this.setData()
    })
  }

  setData(){
    if(this.currentCandidate.primarySchoolStartDate || this.candidateLocalStorage){
      this.savedInformations = { 
      primarySchoolStartDate : this.candidateLocalStorage?.primarySchoolStartDate ?? this.currentCandidate.primarySchoolStartDate,
      primarySchoolEndDate : this.candidateLocalStorage?.primarySchoolEndDate ?? this.currentCandidate.primarySchoolEndDate,
      middleSchoolStartDate : this.candidateLocalStorage?.middleSchoolStartDate ?? this.currentCandidate.middleSchoolStartDate,
      middleSchoolEndDate : this.candidateLocalStorage?.middleSchoolEndDate ?? this.currentCandidate.middleSchoolEndDate,
      highSchoolStartDate : this.candidateLocalStorage?.highSchoolStartDate ?? this.currentCandidate.highSchoolStartDate,
      highSchoolEndDate : this.candidateLocalStorage?.highSchoolEndDate ?? this.currentCandidate.highSchoolEndDate,
      universityStartDate : this.candidateLocalStorage?.universityStartDate ?? this.currentCandidate.universityStartDate,
      universityEndDate : this.candidateLocalStorage?.universityEndDate ?? this.currentCandidate.universityEndDate,
      masterStartDate : this.candidateLocalStorage?.masterStartDate ?? this.currentCandidate.masterStartDate,
      masterEndDate : this.candidateLocalStorage?.masterEndDate ?? this.currentCandidate.masterEndDate,
      englishReadingLevel : this.candidateLocalStorage?.englishReadingLevel ?? this.currentCandidate.englishReadingLevel,
      englishWritingLevel : this.candidateLocalStorage?.englishWritingLevel ?? this.currentCandidate.englishWritingLevel,
      englishSpeakingLevel : this.candidateLocalStorage?.englishSpeakingLevel ?? this.currentCandidate.englishSpeakingLevel,
      frenchReadingLevel : this.candidateLocalStorage?.frenchReadingLevel ?? this.currentCandidate.frenchReadingLevel,
      frenchWritingLevel : this.candidateLocalStorage?.frenchWritingLevel ?? this.currentCandidate.frenchWritingLevel,
      frenchSpeakingLevel : this.candidateLocalStorage?.frenchSpeakingLevel ?? this.currentCandidate.frenchSpeakingLevel,
      deutchReadingLevel : this.candidateLocalStorage?.deutchReadingLevel ?? this.currentCandidate.deutchReadingLevel,
      deutchWritingLevel : this.candidateLocalStorage?.deutchWritingLevel ?? this.currentCandidate.deutchWritingLevel,
      deutchSpeakingLevel : this.candidateLocalStorage?.deutchSpeakingLevel ?? this.currentCandidate.deutchSpeakingLevel,
      }
    }
    this.createForm()
  }
}