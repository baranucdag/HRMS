import { Subject } from 'rxjs';
import { ApplicationService } from './../../../../core/services/api/application.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { TableComponent } from 'src/app/core/components/tables';
import { ITableOptions } from 'src/app/core/components/tables/table/models';

@Component({
  selector: 'app-job-advert-applications',
  templateUrl: './job-advert-applications.component.html',
  styleUrls: ['./job-advert-applications.component.scss'],
})
export class JobAdvertApplicationsComponent implements OnInit {
  @ViewChild('table') table?: TableComponent;

  initialData!: { id: number };
  tableOptions!: ITableOptions;

  private readonly onDestroy = new Subject<void>();
  constructor(private applciationService: ApplicationService) {}

  ngOnInit(): void {
    this.createTable();
  }

  ngOnDestroy() {
    this.onDestroy.next();
    this.onDestroy.complete();
  }

  //set data (id)
  setData(data: any) {
    this.initialData = data;
    if (this.initialData.id) {
      // update
    } else {
      // create
    }
  }

  createTable() {
    this.tableOptions = {
      data: [],
      columns: [
        { field: 'positionName', title: 'positionName', type: 'text' },
        { field: 'publishDate', title: 'Publish Date', type: 'text' },
        { field: 'candidateFullName', title: 'candidateFullName', type: 'text' },
      ],
      filterable: true,
      sortable: true,
      sortMode: 'multiple',
      scrollable: false,
      selectable: true,
      pageOptions: {
        pageSize: 25,
        pageSizes: [25, 50, 100, 250, 500],
      },
      globalFilterOptions: {
        clearButtonOptions: {
          label: 'Clear',
        },
        searchOptions: {
          placeholder: 'Search',
        },
      },
      lazyLoad: true,
      dataService: this.applciationService,
    };
  }
}
