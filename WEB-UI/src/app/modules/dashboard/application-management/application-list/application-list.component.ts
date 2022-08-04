import { Component, OnInit, ViewChild } from '@angular/core';
import { TableComponent } from 'src/app/core/components/tables';
import { ITableOptions } from 'src/app/core/components/tables/table/models';
import { ApplicationService } from 'src/app/core/services/api';

@Component({
  selector: 'app-application-list',
  templateUrl: './application-list.component.html',
  styleUrls: ['./application-list.component.scss'],
})
export class ApplicationListComponent implements OnInit {
  @ViewChild('table') table?: TableComponent;

  // component içerisinde kullanılan değişkenler
  tableOptions!: ITableOptions;
  constructor(private applicationService: ApplicationService) {}

  ngOnInit(): void {
    this.createTable();
  }

  createTable() {
    this.tableOptions = {
      data: [],
      columns: [
        { field: 'positionName', title: 'Position Name', type: 'text' },
        {
          field: 'qualificationLevel',
          title: 'Qualification Level',
          type: 'text',
        },
        { field: 'deadline', title: 'deadline', type: 'text' },
        {
          field: 'candidateFullName',
          title: 'Candidate Full Name',
          type: 'text',
        },
        { field: 'applicationDate', title: 'Application Date', type: 'text' },
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
      lazyLoad:true,
      dataService:this.applicationService
    };
  }
}
