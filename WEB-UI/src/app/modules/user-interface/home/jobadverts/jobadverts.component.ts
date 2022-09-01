import { IApplication } from './../../../../core/models/views/application.model';
import { CandidateService } from './../../../../core/services/api/candidate.service';
import { IJobAdvert } from './../../../../core/models/views/jobAdvert.model';
import { AuthService } from 'src/app/core/services/api/auth.service';
import { IQueryObject } from './../../../../core/models/views/queryObject.model';
import { Component, OnInit } from '@angular/core';
import { Subject, takeLast, takeUntil, pipe } from 'rxjs';
import {
  JobAdvertService,
  ApplicationService,
} from 'src/app/core/services/api';
import { Router } from '@angular/router';

@Component({
  selector: 'app-jobadverts',
  templateUrl: './jobadverts.component.html',
  styleUrls: ['./jobadverts.component.scss'],
})
export class JobadvertsComponent implements OnInit {
  curentUser: any;
  currentCandidate: any;
  userApplication!: IApplication;
  jobAdverts?: any[] = [];
  pageNumber: number = 1;
  pageSize: number = 5;
  queryString: string = '';
  totalCount!: number;
  private readonly onDestroy = new Subject<void>();

  constructor(
    private jobAdvertService: JobAdvertService,
    private canidateService: CandidateService,
    private authService: AuthService,
    private router: Router,
    private applicationService: ApplicationService
  ) {}

  ngOnInit(): void {
    if (this.authService.isAuthenticated()) {
      this.curentUser = this.authService.decodedToken;
    }
    this.get();
  }

  ngOnDestroy() {
    this.onDestroy.next();
    this.onDestroy.complete();
  }

  get() {
    let search: IQueryObject = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      queryString: this.queryString,
      totalCount: 0,
      items: [],
    };
    return this.jobAdvertService
      .getAllPaged(search)
      .pipe(takeUntil(this.onDestroy))
      .subscribe((response) => {
        this.jobAdverts = response.data.items;
        this.totalCount = response.data.totalCount;
        this.getCandidateByUserId(this.curentUser.UserId);
      });
  }

  navigateToApply(jobAdvert: IJobAdvert) {
    if (this.authService.isAuthenticated()) {
      this.router.navigate(['/apply/' + jobAdvert.id + '/cv']);
    } else this.router.navigate(['/auth/login']);
  }

  checkIfApplied(jobAdvert:IJobAdvert){
    this.getApplication(jobAdvert.id,this.curentUser.UserId)
    if(this.userApplication){
      return true
    }return false
  }

  getApplication(jobAdvertId: number, userId: number) {
    this.applicationService
      .getByUserAndCandidateId(jobAdvertId, userId)
      .pipe(takeUntil(this.onDestroy))
      .subscribe((response) => {
        this.userApplication = response.data;
      });
  }

  getCandidateByUserId(id: number) {
    this.canidateService
      .getByUserId(id)
      .pipe(takeUntil(this.onDestroy))
      .subscribe((response) => {
        this.currentCandidate = response.data;
      });
  }
  paginate(event: any) {
    this.pageNumber = event.page + 1;
    this.get();
  }
}
