import { Component, OnInit, ViewChild } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { ButtonColor, ButtonSize, ButtonType } from 'src/app/core/components/buttons/button/enums';
import { SidebarDialogResult, SidebarDialogResultStatus } from 'src/app/core/components/dialogs/sidebar-dialog/enums';
import { TableComponent } from 'src/app/core/components/tables';
import { IColumnButton, ITableOptions } from 'src/app/core/components/tables/table/models';
import { ICON } from 'src/app/core/constants';
import { MenuService } from 'src/app/core/services/api';
import { DialogService } from 'src/app/core/services/dialog';
import { MenuCreateComponent } from '../menu-create/menu-create.component';
import * as _ from 'lodash';
import { IToolbarOptions } from 'src/app/core/components/toolbars/toolbar/models';
import { DialogComponent } from 'src/app/core/components/dialogs';
import { IDialogOptions } from 'src/app/core/components/dialogs/dialog/models';
import { DialogSize, DialogType } from 'src/app/core/components/dialogs/dialog/enums';
import { enumToArray } from 'src/app/core/helpers/enum';
import { isDeletedOptions } from 'src/app/core/enums';


@Component({
  selector: 'app-menu-list',
  templateUrl: './menu-list.component.html',
  styleUrls: ['./menu-list.component.scss']
})
export class MenuListComponent implements OnInit {
  selectedMenu:any;
  deletionDialogOptions!: IDialogOptions;
  tableOptions!: ITableOptions;
  @ViewChild('table') table?: TableComponent;
  @ViewChild('deletionDialog') deletionDialog?: DialogComponent;
  private readonly onDestroy = new Subject<void>();

  isDeletedOptions: any = enumToArray(isDeletedOptions).map((m) => {
    return { label: m.description.toCapitalize(), value: m.id };
  });


  constructor(
    private menuService: MenuService,
    private readonly dialogService: DialogService,

  ) { }

  ngOnInit(): void {
    this.onEditButtonClick = this.onEditButtonClick.bind(this);
    this.createTable();
    this.createDeleteDialog();
  }

  createTable() {
    this.tableOptions = {
      data: [],
      columns: [
        { field: 'displayText', title: 'Display Text', type: 'text' },
        { field: 'orderNum', title: 'Order Number', type: 'numeric' },
        { field: 'parentName', title: 'Parent Name', type: 'text' },
        {
          title: 'Is Deleted',
          type: 'text',
          field: 'isDeletedText',
          sortable: true,
          filterable: true,
          filter: {
            type: 'dropdown',
            data: this.isDeletedOptions,
            defaultValue: this.isDeletedOptions[1].value,
          },
          template: '<i>{{isDeletedText}}</i>',
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
      dataService: this.menuService
    };
  }

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
    ] as IColumnButton[];
  }

  get toolbarOptions(): IToolbarOptions {
    return {
      defaultButtons: {
        new: {
          onClick: () => {
            const ref = this.dialogService.open(MenuCreateComponent, {
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
          }
        },
      },
    } as IToolbarOptions;
  }


  onEditButtonClick(row: any, index: number) {
    const ref = this.dialogService.open(MenuCreateComponent, {
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

  onDeleteButtonClick(row: any, index: number) {
    this.selectedMenu = row;
    this.deletionDialog?.open();
  }

  createDeleteDialog(){
    this.deletionDialogOptions = {
      title: "Menü Silme İşlemi",
      message: "Menü silinecek, emin misiniz ?",
      type: DialogType.confirm,
      size: DialogSize.small,
      onConfirm: () => {
        this.deleteMenuById(this.selectedMenu.id);
      },
      onReject: () => {
        
      },
      onCancel: () => {
      
      }
    }
  }

  deleteMenuById(id: number) {
    this.menuService
      .delete(id)
      .pipe(takeUntil(this.onDestroy))
      .subscribe((result:any) => {
        this.table!.refreshData();
      });
  }

  ngOnDestroy() {
    this.onDestroy.next();
    this.onDestroy.complete();
  }
}
