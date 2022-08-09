import { Component, OnInit, ViewChild } from '@angular/core';
import { TableComponent } from 'src/app/core/components/tables';
import { ITableOptions } from 'src/app/core/components/tables/table/models';
import {
  isDeletedOptions,
  workPlaceTypeOptions,
  workTimeTypeOptions,
} from 'src/app/core/enums';
import { enumToArray } from 'src/app/core/helpers/enum';
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

  isDeletedOptions: any = enumToArray(isDeletedOptions).map((m) => {
    return { label: m.description.toCapitalize(), value: m.id };
  });

  workTimeTypeOptions: any = enumToArray(workTimeTypeOptions).map((m) => {
    return { label: m.description.toCapitalize(), value: m.id };
  });

  workPlaceTypeOptions: any = enumToArray(workPlaceTypeOptions).map((m) => {
    return { label: m.description.toCapitalize(), value: m.id };
  });

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
        {
          title: 'Work Place Type',
          type: 'text',
          field: 'workPlaceType',
          sortable: true,
          filterable: true,
          filter: {
            type: 'dropdown',
            data: this.workPlaceTypeOptions,
            defaultValue: this.workPlaceTypeOptions[0].value,
          },
        },
        {
          title: 'Work Time Type',
          type: 'text',
          field: 'workTimeType',
          sortable: true,
          filterable: true,
          filter: {
            type: 'dropdown',
            data: this.workTimeTypeOptions,
            defaultValue: this.workTimeTypeOptions[0].value,
          },
        },
        {
          field: 'candidateFullName',
          title: 'Candidate Full Name',
          type: 'text',
        },
        { field: 'applicationDate', title: 'Application Date', type: 'date' },
        {
          title: 'Is Deleted',
          type: 'text',
          field: 'isDeleted',
          sortable: true,
          filterable: true,
          filter: {
            type: 'dropdown',
            data: this.isDeletedOptions,
            defaultValue: this.isDeletedOptions[1].value,
          },
          template: '<i>{{isDeletedText}}</i>',
        },
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
      dataService: this.applicationService,
    };
  }
}
