import { LazyLoadEvent } from 'primeng/api';
import { IBaseModel } from './../../../../core/models/base.model';
import { IJobAdvert } from './../../../../core/models/views/jobAdvert.model';
import { JobAdvertService } from './../../../../core/services/api/job-advert.service';
import { Component, OnInit } from '@angular/core';
import { ITableOptions } from 'src/app/core/components/tables/table/models';
import { T } from 'src/app/core/helpers/i18n';

@Component({
  selector: 'app-job-advert-list',
  templateUrl: './job-advert-list.component.html',
  styleUrls: ['./job-advert-list.component.scss']
})
export class JobAdvertListComponent implements OnInit {
  tableOptions!: ITableOptions;
  
  constructor(private jobAdvertService:JobAdvertService) { }

  ngOnInit(): void {
    this.createTable()
  }

  createTable() {
    this.tableOptions = {
      data: [ 
        this.jobAdvertService.getPaginationDataPath()
      ],
      columns: [
        { field: 'positionName', title: 'Position Name', type: 'text' },
        { field: 'qualificationLevel', title: 'Qualification Level', type: 'text' },
        { field: 'workType', title: 'Work Type', type: 'text' },
        { field: 'publishDate', title: 'Publish Date', type: 'date' },
        { field: 'deadLine', title: 'DeadLine', type: 'date' },
        /*{
          title: 'Is Deleted',
          type: 'text',
          field: 'isDeletedText',
          sortable: true,
          filterable: true,
          filter: {
            type: 'dropdown',
            data: this.isDeletedOptions,
            defaultValue: this.isDeletedOptions[0].value,
          },
          template: '<i>{{isDeletedText}}</i>',
        },
        { title: 'Actions', type: 'actions', buttons: this.getButtons() },*/
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
      // statik data ile çalıştığımız için false dedik
      dataService: this.jobAdvertService,
      dataServiceMethod:this.jobAdvertService.getPeginationData()
    };
  }
}
