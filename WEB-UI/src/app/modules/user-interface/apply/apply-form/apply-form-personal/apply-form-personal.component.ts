import { Subject, takeUntil } from 'rxjs';
import { AuthService } from 'src/app/core/services/api/auth.service';
import { UserService } from 'src/app/core/services/api/user.service';
import { LocalStorageService } from './../../../../../core/services/local-storage/local-storage.service';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { CandidateFormService } from './../../../../../core/services/api/candidate-form.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { gender } from 'src/app/core/enums';
import { enumToArray } from 'src/app/core/helpers/enum';

@Component({
  selector: 'app-apply-form-personal',
  templateUrl: './apply-form-personal.component.html',
  styleUrls: ['./apply-form-personal.component.scss'],
})
export class ApplyFormPersonalComponent implements OnInit {
  personalInformationForm!: FormGroup;
  savedInformations: any;
  currentUser: any;
  disabled: boolean = true;
  isMarried?: boolean = false;
  hasLawSuit?: boolean;
  hasDisability?: boolean;
  hasMentalPhysicalProblem?: boolean;
  hasCriminalRecord?: boolean;
  hadMajorSurgery?: boolean;
  hasValidPassport?: boolean;
  canTravel?: boolean;

  genders: any = enumToArray(gender).map((m) => {
    return { label: m.description.toCapitalize(), value: m.id };
  });

  private readonly onDestroy = new Subject<void>();

  constructor(
    private formBuilder: FormBuilder,
    private candidateFormService: CandidateFormService,
    private messageService: MessageService,
    private router: Router,
    private localStorageService: LocalStorageService,
    private authService: AuthService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.savedInformations = JSON.parse(
      this.localStorageService.get('personalInformations')!
    );
    if (this.savedInformations) {
      this.isMarried = this.savedInformations.isMarried;
      this.hasLawSuit = this.savedInformations.hasLawSuit;
      this.hasDisability = this.savedInformations.hasDisability;
      this.hasMentalPhysicalProblem =
        this.savedInformations.hasMentalPhysicalProblem;
      this.hadMajorSurgery = this.savedInformations.hadMajorSurgery;
      this.hasCriminalRecord = this.savedInformations.hasCriminalRecord;
      this.hasValidPassport = this.savedInformations.hasValidPassport;
      this.canTravel = this.savedInformations.canTravel;
    }
    this.createForm();
  }

  ngOnDestroy() {
    this.onDestroy.next();
    this.onDestroy.complete();
  }

  sendFormToService() {
    if (this.personalInformationForm.valid) {
      const form = Object.assign({}, this.personalInformationForm.value);
      this.candidateFormService.setPersonalInformation(form);
      this.localStorageService.set(
        'personalInformations',
        JSON.stringify(form)
      );
      this.router.navigate(['/apply/form/education']);
    } else
      this.messageService.add({
        severity: 'error',
        detail: 'form is invalid !',
      });
  }

  createForm() {
    this.getCurrentUser();
    this.personalInformationForm = this.formBuilder.group({
      dateOfBirth: [
        this.savedInformations ? this.savedInformations.dateOfBirth : '',
        Validators.required,
      ],
      placeOfBirth: [
        this.savedInformations ? this.savedInformations.placeOfBirth : '',
        Validators.required,
      ],
      adress: [
        this.savedInformations ? this.savedInformations.adress : '',
        Validators.required,
      ],
      phoneNumber: [
        this.savedInformations ? this.savedInformations.phoneNumber : '',
        Validators.required,
      ],
      homePhoneNumber: [
        this.savedInformations ? this.savedInformations.homePhoneNumber : '',
        Validators.required,
      ],
      gender: [this.savedInformations ? this.savedInformations.gender : ''],
      profession: [
        this.savedInformations ? this.savedInformations.profession : '',
      ],
      isMarried: [
        this.savedInformations ? this.savedInformations.isMarried : '',
      ],
      spouseName: [
        this.savedInformations ? this.savedInformations.spouseName : '',
      ],
      spouseProfession: [
        this.savedInformations ? this.savedInformations.spouseProfession : '',
      ],
      spouseCurrentJob: [
        this.savedInformations ? this.savedInformations.spouseCurrentJob : '',
      ],
      fatherName: [
        this.savedInformations ? this.savedInformations.fatherName : '',
        Validators.required,
      ],
      fatherProfession: [
        this.savedInformations ? this.savedInformations.fatherProfession : '',
      ],
      fatherCurrentJob: [
        this.savedInformations ? this.savedInformations.fatherCurrentJob : '',
      ],
      motherName: [
        this.savedInformations ? this.savedInformations.motherName : '',
        Validators.required,
      ],
      motherProfession: [
        this.savedInformations ? this.savedInformations.motherProfession : '',
      ],
      motherCurrentJob: [
        this.savedInformations ? this.savedInformations.motherCurrentJob : '',
      ],
      hasCriminalRecord: [
        this.savedInformations ? this.savedInformations.hasCriminalRecord : '',
      ],
      criminalRecordReason: [
        this.savedInformations
          ? this.savedInformations.criminalRecordReason
          : '',
      ],
      hasLawSuit: [
        this.savedInformations ? this.savedInformations.hasLawSuit : '',
      ],
      lawSuitReason: [
        this.savedInformations ? this.savedInformations.lawSuitReason : '',
      ],
      hasDisability: [
        this.savedInformations ? this.savedInformations.hasDisability : '',
      ],
      disabilityDetail: [
        this.savedInformations ? this.savedInformations.disabilityDetail : '',
      ],
      hasMentalPhysicalProblem: [
        this.savedInformations
          ? this.savedInformations.hasMentalPhysicalProblem
          : '',
      ],
      mentalPhysicalProblemDetail: [
        this.savedInformations
          ? this.savedInformations.mentalPhysicalProblemDetail
          : '',
      ],
      hadMajorSurgery: [
        this.savedInformations ? this.savedInformations.hadMajorSurgery : '',
      ],
      majorSurgeryDetail: [
        this.savedInformations ? this.savedInformations.majorSurgeryDetail : '',
      ],
      bloudGroup: [
        this.savedInformations ? this.savedInformations.bloudGroup : '',
        Validators.required,
      ],
      militaryInformation: [
        this.savedInformations
          ? this.savedInformations.militaryInformation
          : '',
      ],
      drivingLicenceInformation: [
        this.savedInformations
          ? this.savedInformations.drivingLicenceInformation
          : '',
      ],
      hasValidPassport: [
        this.savedInformations ? this.savedInformations.hasValidPassport : '',
      ],
      canTravel: [
        this.savedInformations ? this.savedInformations.canTravel : '',
      ],
      sucscribedUnions: [
        this.savedInformations ? this.savedInformations.sucscribedUnions : '',
      ],
      sucscribedSociety: [
        this.savedInformations ? this.savedInformations.sucscribedSociety : '',
      ],
      sucscribedProfessionalOrganizations: [
        this.savedInformations
          ? this.savedInformations.sucscribedProfessionalOrganizations
          : '',
      ],
      sucscribedSporClubs: [
        this.savedInformations
          ? this.savedInformations.sucscribedSporClubs
          : '',
      ],
      applicationReasonDetail: [
        this.savedInformations
          ? this.savedInformations.applicationReasonDetail
          : '',
      ],
      commonAquaintances: [
        this.savedInformations
          ? this.savedInformations.commonAquaintances
          : '',
      ],
      jobForApplied: [
        this.savedInformations
          ? this.savedInformations.jobForApplied
          : '',
      ],
    });
  }
  getCurrentUser() {
    this.authService.getUserDetailsFromToken();
    this.userService
      .getById(this.authService.decodedToken.UserId)
      .pipe(takeUntil(this.onDestroy))
      .subscribe((response) => {
        this.currentUser = response.body?.data;
      });
  }
}
