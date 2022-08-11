import { IDropdownOptions } from 'src/app/core/components/dropdowns/dropdown/models';
import { UserCreateComponent } from './../user-create/user-create.component';
import { Component, OnInit, ViewChild } from '@angular/core';
import {
  ButtonColor,
  ButtonSize,
  ButtonType,
} from 'src/app/core/components/buttons/button/enums';
import { TableComponent } from 'src/app/core/components/tables';
import {
  IColumnButton,
  ITableOptions,
} from 'src/app/core/components/tables/table/models';
import { IToolbarOptions } from 'src/app/core/components/toolbars/toolbar/models';
import { ICON } from 'src/app/core/constants';
import { UserService } from 'src/app/core/services/api/user.service';
import { DialogService } from 'src/app/core/services/dialog';
import { SidebarDialogResult, SidebarDialogResultStatus } from 'src/app/core/components/dialogs/sidebar-dialog/enums';
import * as _ from 'lodash';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss'],
})
export class UserListComponent implements OnInit {
  @ViewChild('table') table?: TableComponent;

  tableOptions!: ITableOptions;

  constructor(private userService: UserService, private dialogService: DialogService) {}

  ngOnInit(): void {
    this.ceateTable();
  }

  ceateTable() {
    this.tableOptions = {
      data: [],
      columns: [
        { field: 'fullName', title: 'Full Name', type: 'text' },
        { field: 'email', title: 'Email', type: 'text' },
        { field: 'userClaim', title: 'User Claim', type: 'text' },
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
      dataService: this.userService,
    };
  }

    // toolbarın özellikleri tanımlanıyor
    get toolbarOptions(): IToolbarOptions {
      return {
        defaultButtons: {
          new: {
            onClick: () => {
              const ref = this.dialogService.open(UserCreateComponent, {
                title: 'Set claim for user',
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
    const ref = this.dialogService.open(UserCreateComponent, {
      title: 'Edit Job Advert',
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
    ] as IColumnButton[]; // cast ediliyor
  }

}
