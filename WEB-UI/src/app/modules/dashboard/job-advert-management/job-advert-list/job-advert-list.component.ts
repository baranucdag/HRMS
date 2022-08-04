import { Subject, takeUntil } from 'rxjs';
import { DialogService } from 'src/app/core/services/dialog';
import { JobAdvertCreateComponent } from './../job-advert-create/job-advert-create.component';
import { JobAdvertService } from './../../../../core/services/api/job-advert.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import {
  IColumnButton,
  ITableOptions,
} from 'src/app/core/components/tables/table/models';
import { IGridComponent } from 'src/app/core/components/interfaces';
import { TableComponent } from 'src/app/core/components/tables';
import { IToolbarOptions } from 'src/app/core/components/toolbars/toolbar/models';
import {
  SidebarDialogResult,
  SidebarDialogResultStatus,
} from 'src/app/core/components/dialogs/sidebar-dialog/enums';
import * as _ from 'lodash';
import { IDialogOptions } from 'src/app/core/components/dialogs/dialog/models';
import {
  DialogSize,
  DialogType,
} from 'src/app/core/components/dialogs/dialog/enums';
import {
  ButtonColor,
  ButtonSize,
  ButtonType,
} from 'src/app/core/components/buttons/button/enums';
import { ICON } from 'src/app/core/constants';
import { DialogComponent } from 'src/app/core/components/dialogs';
import { enumToArray } from 'src/app/core/helpers/enum';
import { isDeletedOptions } from 'src/app/core/enums';

@Component({
  selector: 'app-job-advert-list',
  templateUrl: './job-advert-list.component.html',
  styleUrls: ['./job-advert-list.component.scss'],
})
export class JobAdvertListComponent implements OnInit, IGridComponent {
  @ViewChild('table') table?: TableComponent;
  @ViewChild('deletionDialog') deletionDialog?: DialogComponent;
  @ViewChild('unDeletionDialog') unDeletionDialog?: DialogComponent;

  // component içerisinde kullanılan değişkenler
  selectedMenu: any;
  selectedUnDeleteAdvert:any;
  tableOptions!: ITableOptions;
  deletionDialogOptions!: IDialogOptions;
  unDeletionDialogOptions!: IDialogOptions;

  isDeletedOptions: any = enumToArray(isDeletedOptions).map((m) => {
    return { label: m.description.toCapitalize(), value: m.id };
  });

  private readonly onDestroy = new Subject<void>();

  constructor(
    private jobAdvertService: JobAdvertService,
    private dialogService: DialogService
  ) {}

  ngOnInit(): void {
    this.onEditButtonClick = this.onEditButtonClick.bind(this);
    this.onDeleteButtonClick = this.onDeleteButtonClick.bind(this);
    this.onUnDeleteButtonClick = this.onUnDeleteButtonClick.bind(this);
    this.createTable();
    this.createDeleteDialog();
    this.createUnDeleteDialog();
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
        { field: 'description', title: 'Description', type: 'text' },
        { field: 'deadline', title: 'Deadline', type: 'text' },
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
          //template: '<i>{{isDeletedText}}</i>',
        },
        { title: 'Actions', type: 'actions', buttons: this.getButtons() },
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

  // dialog componentin özellikleri tanımlanıyor
  createDeleteDialog() {
    this.deletionDialogOptions = {
      title: 'Job Advert Silme İşlemi',
      message: 'Menü silinecek, emin misiniz ?',
      type: DialogType.confirm,
      size: DialogSize.small,
      onConfirm: () => {
        this.deleteJobAdvertById(this.selectedMenu.id);
      },
      onReject: () => {},
      onCancel: () => {},
    };
  }

  createUnDeleteDialog() {
    this.unDeletionDialogOptions = {
      title: 'Job Advert Silme İşlemi',
      message: 'Menü silinecek, emin misiniz ?',
      type: DialogType.confirm,
      size: DialogSize.small,
      onConfirm: () => {
        this.UndeleteJobAdvertById(this.selectedMenu.id);
      },
      onReject: () => {},
      onCancel: () => {},
    };
  }

  // id numarasına menüleri soft-delete silmek için api'ye istek gönderen metod
  UndeleteJobAdvertById(id: number) {
    this.jobAdvertService
      .UnDelete(id)
      .pipe(takeUntil(this.onDestroy))
      .subscribe((result: any) => {
        this.table!.refreshData();
      });
  }

  // id numarasına menüleri soft-delete silmek için api'ye istek gönderen metod
  deleteJobAdvertById(id: number) {
    this.jobAdvertService
      .delete(id)
      .pipe(takeUntil(this.onDestroy))
      .subscribe((result: any) => {
        this.table!.refreshData();
      });
  }

  ngOnDestroy() {
    this.onDestroy.next();
    this.onDestroy.complete();
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

  // edit butonuna tıklanıldığında çalışacak metod
  onEditButtonClick(row: any, index: number) {
    const ref = this.dialogService.open(JobAdvertCreateComponent, {
      title: 'Edit Menu',
      buttons: {
        update: true,
        cancel: true,
      },
      onResult: (result: SidebarDialogResult) => {
        if (_.isEqual(result.status, SidebarDialogResultStatus.updateSuccess)) {
          ref.close();
          this.table?.refreshData();
        }
      },
      data: {
        id: row.id,
      },
    });
  }

  // delete butonuna tıklanıldığında çalışacak metod
  onDeleteButtonClick(row: any, index: number) {
    this.selectedMenu = row;
    this.deletionDialog?.open();
  }

  // undelete butonuna basınca çalışacak metod
  onUnDeleteButtonClick(row: any, index: number) {
    this.selectedMenu = row;
    this.unDeletionDialog?.open();
  }

  // tablodaki rowların yanındaki butonların özellikleri tanımlanıyor
  getButtons(): IColumnButton[] {
    return [
      {
        options: {
          properties: [
            ButtonType.raised,
            ButtonType.rounded,
            ButtonSize.small,
            ButtonColor.info,
          ],
          icon: ICON.pencil,
          tooltip: 'Edit',
        },
        onClick: (row: any, index: number) => {
          this.onEditButtonClick(row, index);
        },
      },
      {
        options: {
          properties: [
            ButtonType.raised,
            ButtonType.rounded,
            ButtonSize.small,
            ButtonColor.danger,
          ],
          icon: ICON.trash,
          tooltip: 'Delete',
        },
        onClick: (row: any, index: number) => {
          this.onDeleteButtonClick(row, index);
        },
      },
      {
        options: {
          properties: [
            ButtonType.raised,
            ButtonType.rounded,
            ButtonSize.small,
            ButtonColor.secondary,
          ],
          icon: ICON.undo,
          tooltip: 'Undelete',
        },
        onClick: (row: any, index: number) => {
          this.onUnDeleteButtonClick(row, index);
        },
      },
    ] as IColumnButton[]; // cast ediliyor
  }
}
