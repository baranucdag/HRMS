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
import { IGridComponent } from 'src/app/core/components/interfaces';

@Component({
  selector: 'app-menu-list',
  templateUrl: './menu-list.component.html',
  styleUrls: ['./menu-list.component.scss']
})
export class MenuListComponent implements OnInit, IGridComponent {
  // child componentler tanımlanıyor
  @ViewChild('table') table?: TableComponent;
  @ViewChild('deletionDialog') deletionDialog?: DialogComponent;
  
  // api isteklerindeki asenkron işlemleri yönetirken subscribe olunuyor, takeUntil ile otomatik unsubscribe oluruz. Gereksiz bellek kullanımı önlemiş oluruz
  // bknz: https://www.digitalocean.com/community/tutorials/angular-takeuntil-rxjs-unsubscribe
  private readonly onDestroy = new Subject<void>();
  
  // component içerisinde kullanılan değişkenler
  selectedMenu:any;
  deletionDialogOptions!: IDialogOptions;
  tableOptions!: ITableOptions;
  

  isDeletedOptions: any = enumToArray(isDeletedOptions).map((m) => {
    return { label: m.description.toCapitalize(), value: m.id };
  });


  constructor(
    private menuService: MenuService,
    private readonly dialogService: DialogService,
  ) { }

  ngOnInit(): void {
    this.onEditButtonClick = this.onEditButtonClick.bind(this);
    this.onDeleteButtonClick = this.onDeleteButtonClick.bind(this);
    this.createTable();
    this.createDeleteDialog();
  }

  createTable() {
    this.tableOptions = {
      data: [ // statik data ile çalıştığımız için array'i doldurduk, api ile çalışacaksak boş array gönderilebilir
        {
          id: 1,
          displayText: "Menu Management",
          icon: "pi pi-list",
          parentId:null,
          orderNum: 10,
          url:null,
          menuGuid: "asdasdasdasdasd",
          isDeletedText:"No"
        },
        {
          id:2,
          displayText: "Menu List",
          icon: "bi bi-table",
          parentId: 1,
          orderNum: 11,
          url: "menu-management/menu-list",
          menuGuid: "assadasdasdadsdasdasdasdasd",
          isDeletedText:"No"
        },
      ],
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
            defaultValue: this.isDeletedOptions[0].value,
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
      lazyLoad: false, // statik data ile çalıştığımız için false dedik
      //dataService: this.menuService// statik data ile çalıştığımız için yorum satırına aldık
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
    ] as IColumnButton[]; // cast ediliyor
  }

  // toolbarın özellikleri tanımlanıyor
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

  // edit butonuna tıklanıldığında çalışacak metod
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

  // delete butonuna tıklanıldığında çalışacak metod
  onDeleteButtonClick(row: any, index: number) {
    this.selectedMenu = row;
    this.deletionDialog?.open();
  }

  // dialog componentin özellikleri tanımlanıyor
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

  // id numarasına menüleri soft-delete silmek için api'ye istek gönderen metod
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
