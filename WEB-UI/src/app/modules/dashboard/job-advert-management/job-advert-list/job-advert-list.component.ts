import { DialogService } from 'src/app/core/services/dialog';
import { JobAdvertCreateComponent } from './../job-advert-create/job-advert-create.component';
import { JobAdvertService } from './../../../../core/services/api/job-advert.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ITableOptions } from 'src/app/core/components/tables/table/models';
import { IGridComponent } from 'src/app/core/components/interfaces';
import { TableComponent } from 'src/app/core/components/tables';
import { IToolbarOptions } from 'src/app/core/components/toolbars/toolbar/models';
import { SidebarDialogResult, SidebarDialogResultStatus } from 'src/app/core/components/dialogs/sidebar-dialog/enums';
import * as _ from 'lodash';

@Component({
  selector: 'app-job-advert-list',
  templateUrl: './job-advert-list.component.html',
  styleUrls: ['./job-advert-list.component.scss'],
})
export class JobAdvertListComponent implements OnInit, IGridComponent {
  @ViewChild('table') table?: TableComponent;
  tableOptions!: ITableOptions;

  constructor(
    private jobAdvertService: JobAdvertService,
    private dialogService: DialogService
  ) {}

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
        { field: 'workType', title: 'Work Type', type: 'text' },
        { field: 'publishDate', title: 'Publish Date', type: 'text' },
        { field: 'deadLine', title: 'DeadLine', type: 'text' },
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
      dataService: this.jobAdvertService,
    };
  }

  // toolbarın özellikleri tanımlanıyor
  get toolbarOptions(): IToolbarOptions {
    return {
      defaultButtons: {
        new: {
          onClick: () => {
            const ref = this.dialogService.open(JobAdvertCreateComponent, {
              title: 'Create Menu Item',
              buttons: {
                save: true,
                cancel: true,
              },
              onResult: (result: SidebarDialogResult) => {
                if (
                  _.isEqual(
                    result.status,
                    SidebarDialogResultStatus.saveSuccess
                  )
                ) {
                  ref.close();
                  this.table?.refreshData();
                }
              },
              data: {
                id: null,
              },
            });
          },
        },
      },
    } as IToolbarOptions;
  }
}
