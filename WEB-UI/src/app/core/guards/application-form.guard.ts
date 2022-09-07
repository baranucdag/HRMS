import { IApplication } from './../models/views/application.model';
import { CandidateService } from './../services/api/candidate.service';
import { ApplicationService } from 'src/app/core/services/api';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../services/api/auth.service';

@Injectable({
  providedIn: 'root'
})
export class ApplicationFormGuard implements CanActivate {
  applications:IApplication[]=[]
  candidateId:any

  constructor(
    private authService: AuthService,
    private candidateService:CandidateService,
    private applicationService:ApplicationService
  ) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      this.authService.getUserDetailsFromToken()
      this.checkApplicationStatus()
      
    return true;
  }
  checkApplicationStatus(){
    this.candidateService.getByUserId(this.authService.decodedToken.UserId).subscribe((response)=>{
      this.candidateId = response.data.candidateId
    })
    this.applicationService.getByCandidateId(this.candidateId).subscribe((response)=>{
      this.applications = response.data
    })
  }
  
}
