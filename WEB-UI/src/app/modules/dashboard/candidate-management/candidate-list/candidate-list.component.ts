import { Subject, takeUntil } from 'rxjs';
import { DialogType } from './../../../../core/components/dialogs/dialog/enums/dialog-type.enum';
import { ICON } from 'src/app/core/constants';
import { CandidateService } from './../../../../core/services/api/candidate.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { TableComponent } from 'src/app/core/components/tables';
import {
  IColumnButton,
  ITableOptions,
} from 'src/app/core/components/tables/table/models';
import { enumToArray } from 'src/app/core/helpers/enum';
import { isDeletedOptions } from 'src/app/core/enums';
import {
  ButtonColor,
  ButtonSize,
  ButtonType,
} from 'src/app/core/components/buttons/button/enums';
import { IDialogOptions } from 'src/app/core/components/dialogs/dialog/models';
import { DialogComponent } from 'src/app/core/components/dialogs';
import { DialogSize } from 'src/app/core/components/dialogs/dialog/enums';

@Component({
  selector: 'app-candidate-list',
  templateUrl: './candidate-list.component.html',
  styleUrls: ['./candidate-list.component.scss'],
})
export class CandidateListComponent implements OnInit {
  @ViewChild('table') table?: TableComponent;
  @ViewChild('deletionDialog') deletionDialog?: DialogComponent;
  @ViewChild('unDeletionDialog') unDeletionDialog?: DialogComponent;

  // component içerisinde kullanılan değişkenler
  selectedCandidate: any;
  tableOptions!: ITableOptions;
  deletionDialogOptions!: IDialogOptions;
  unDeletionDialogOptions!: IDialogOptions;

  isDeletedOptions: any = enumToArray(isDeletedOptions).map((m) => {
    return { label: m.description.toCapitalize(), value: m.id };
  });

  private readonly onDestroy = new Subject<void>();

  constructor(
    private candidateService: CandidateService,
  ) {}

  ngOnInit(): void {
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
        {
          field: 'candidateFullName',
          title: 'Candidate Full Name',
          type: 'text',
        },
        {
          field: 'eMail',
          title: 'Email',
          type: 'text',
        },
        { field: 'profession', title: 'Profession', type: 'text' },
        { field: 'phoneNumber', title: 'Phone Number', type: 'text' },
        { field: 'adress', title: 'Adress', type: 'text' },
        { field: 'gender', title: 'Gender', type: 'text' },
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
      dataService: this.candidateService,
    };
  }

  // delete butonuna tıklanıldığında çalışacak metod
  onDeleteButtonClick(row: any, index: number) {
    this.selectedCandidate = row;
    this.deletionDialog?.open();
  }

  // undelete butonuna basınca çalışacak metod
  onUnDeleteButtonClick(row: any, index: number) {
    this.selectedCandidate = row;
    this.unDeletionDialog?.open();
  }

  // id numarasına menüleri soft-delete silmek için api'ye istek gönderen metod
  UndeleteJobAdvertById(id: number) {
    this.candidateService
      .UnDelete(id)
      .pipe(takeUntil(this.onDestroy))
      .subscribe((result: any) => {
        this.table!.refreshData();
      });
  }

  // id numarasına menüleri soft-delete silmek için api'ye istek gönderen metod
  deleteJobAdvertById(id: number) {
    this.candidateService
      .delete(id)
      .pipe(takeUntil(this.onDestroy))
      .subscribe((result: any) => {
        this.table!.refreshData();
      });
  }

  // dialog componentin özellikleri tanımlanıyor
  createDeleteDialog() {
    this.deletionDialogOptions = {
      title: 'Job Advert Silme İşlemi',
      message: 'Menü silinecek, emin misiniz ?',
      type: DialogType.confirm,
      size: DialogSize.small,
      onConfirm: () => {
        this.deleteJobAdvertById(this.selectedCandidate.id);
      },
      onReject: () => {},
      onCancel: () => {},
    };
  }

  createUnDeleteDialog() {
    this.unDeletionDialogOptions = {
      title: 'Job Advert Silme İşlemi',
      message: 'Menü geri gelecek, emin misiniz ?',
      type: DialogType.confirm,
      size: DialogSize.small,
      onConfirm: () => {
        this.UndeleteJobAdvertById(this.selectedCandidate.id);
      },
      onReject: () => {},
      onCancel: () => {},
    };
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
