import { IGridComponent } from 'src/app/core/components/interfaces';
import { Subject, takeUntil } from 'rxjs';
import { IJobAdvert } from './../../../core/models/views/jobAdvert.model';
import { JobAdvertService } from './../../../core/services/api/job-advert.service';
import { Component, OnDestroy, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit,OnDestroy {
  jobAdverts?: IJobAdvert[] = [];
  private readonly onDestroy = new Subject<void>();

  constructor(private jobAdvertService: JobAdvertService) {}

  ngOnInit(): void {
    this.getJobAdverts();
  }

  getJobAdverts() {
    this.jobAdvertService
      .getAll()
      .pipe(takeUntil(this.onDestroy))
      .subscribe((response) => {
        this.jobAdverts = response.body?.data;
      });
  }

  ngOnDestroy() {
    this.onDestroy.next();
    this.onDestroy.complete();
  }
}
