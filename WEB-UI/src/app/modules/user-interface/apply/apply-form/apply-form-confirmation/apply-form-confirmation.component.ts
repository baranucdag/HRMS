import { AuthService } from 'src/app/core/services/api/auth.service';
import { MessageService } from 'primeng/api';
import { Subject, takeUntil } from 'rxjs';
import { CandidateService } from './../../../../../core/services/api/candidate.service';
import { LocalStorageService } from './../../../../../core/services/local-storage/local-storage.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-apply-form-confirmation',
  templateUrl: './apply-form-confirmation.component.html',
  styleUrls: ['./apply-form-confirmation.component.scss'],
})
export class ApplyFormConfirmationComponent implements OnInit {
  candidateInformation: any;
  currentCandidate: any;

  private readonly onDestroy = new Subject<void>();

  constructor(
    private localStorageService: LocalStorageService,
    private candidateService: CandidateService,
    private messageService: MessageService,
    private authService: AuthService,
    private router:Router
  ) {}

  ngOnInit(): void {
    this.getInformation();
    this.authService.getUserDetailsFromToken();
    this.candidateService
      .getByUserId(this.authService.decodedToken.UserId)
      .pipe(takeUntil(this.onDestroy))
      .subscribe((response) => {
        this.currentCandidate = response.data;
      });
  }
  ngOnDestroy() {
    this.onDestroy.next();
    this.onDestroy.complete();
  }

  getInformation() {
    this.candidateInformation = {
      personal: JSON.parse(
        this.localStorageService.get('personalInformations')!
      ),
      education: JSON.parse(
        this.localStorageService.get('educationInformations')!
      ),
      workExperience: JSON.parse(
        this.localStorageService.get('workExperienceInformations')!
      ),
    };
  }

  save() {
    let sendObject = Object.assign(
      JSON.parse(this.localStorageService.get('personalInformations')!),
      JSON.parse(this.localStorageService.get('educationInformations')!),
      JSON.parse(this.localStorageService.get('workExperienceInformations')!)
    );

    let object = {
      userId: this.currentCandidate.userId,
      isDeleted: this.currentCandidate.isDeleted,
      id: this.currentCandidate.id,
      cvPath:this.currentCandidate.cvPath,
      createdAt:this.currentCandidate.createdAt,
    };
    
    let objectToSend = Object.assign(sendObject, object);
    
    this.candidateService.update(objectToSend).pipe(takeUntil(this.onDestroy)).subscribe((response)=>{
      this.messageService.add({ severity: "info", detail:"application comppleted" });
    },(responseError)=>{
      this.messageService.add({ severity: "error", detail:"application couldnt comppleted" });
    })
    this.localStorageService.Remove('personalInformations');
    this.localStorageService.Remove('educationInformations');
    this.localStorageService.Remove('workExperienceInformations');
    this.router.navigate(['/'])
  }
}
