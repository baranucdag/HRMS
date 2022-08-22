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
export class HomeComponent implements OnInit {

  constructor() {}

  ngOnInit(): void {
  }

}
