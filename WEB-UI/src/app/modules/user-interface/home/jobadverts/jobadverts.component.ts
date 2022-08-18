import { IQueryObject } from './../../../../core/models/views/queryObject.model';
import { Component, OnInit } from '@angular/core';
import { Subject, takeLast, takeUntil } from 'rxjs';
import { JobAdvertService } from 'src/app/core/services/api';

@Component({
  selector: 'app-jobadverts',
  templateUrl: './jobadverts.component.html',
  styleUrls: ['./jobadverts.component.scss'],
})
export class JobadvertsComponent implements OnInit {
  jobAdverts?: any[] = [];
  pageNumber: number=1;
  pageSize: number=5;
  queryString: string='';
  totalCount!: number;
  private readonly onDestroy = new Subject<void>();

  constructor(private jobAdvertService: JobAdvertService) {}

  ngOnInit(): void {
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
      });
  }

  paginate(event: any) {
    this.pageNumber = event.page+1;
    this.get()
  }
}
