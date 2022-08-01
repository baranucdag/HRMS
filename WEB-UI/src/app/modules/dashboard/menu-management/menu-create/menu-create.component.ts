import { Component, EventEmitter, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Guid } from 'guid-typescript';
import { Subject, takeUntil } from 'rxjs';
import {
  SidebarDialogResult,
  SidebarDialogResultStatus,
} from 'src/app/core/components/dialogs/sidebar-dialog/enums';
import { IDropdownOptions } from 'src/app/core/components/dropdowns/dropdown/models';
import { IFormComponent } from 'src/app/core/components/interfaces';
import { IOnInitializingParam } from 'src/app/core/components/models';
import { ICON } from 'src/app/core/constants';
import { setSavingStatus, setUpdatingStatus } from 'src/app/core/helpers/sidebar';
import { IMenu } from 'src/app/core/models/views';
import { MenuService } from 'src/app/core/services/api/menu.service';


@Component({
  selector: 'app-menu-create',
  templateUrl: './menu-create.component.html',
  styleUrls: ['./menu-create.component.scss'],
})
export class MenuCreateComponent implements OnInit, IFormComponent {
  initialData!: {id:number}; // buradaki obje özellik olarak değişkenlik gösterebilir, form componentlerinde genelde id alır.
  parentDropdownOptions?: IDropdownOptions;
  iconDropdownOptions?: IDropdownOptions;
  private readonly onDestroy = new Subject<void>();
  public readonly onResult = new EventEmitter<SidebarDialogResult>();
  public readonly onInitializing = new Subject<IOnInitializingParam>();
  
  menuForm!: FormGroup;
  get f(): any {
    return this.menuForm.controls;
  }
 
  selectedParentMenu?: any;
  selectedIcon: any;
  parentMenus!: any[];
  selectedMenu!: IMenu;
  
  constructor(
    private menuService: MenuService
  ) {}

  ngOnInit(): void {
    this.createForm();
  }

  setData(data: any) {
    this.initialData = data;

    if (this.initialData) {
      this.getMenuList(this.initialData.id);
    } else {
      this.getMenuList();
    }
  }

  getMenu(id: number) {
    this.menuService
      .getById(id)
      .pipe(takeUntil(this.onDestroy))
      .subscribe((response: any) => {
        const menu = response.body.data;
        this.selectedMenu = menu;
        this.selectedParentMenu = this.parentMenus.find(
          (m: any) => m.id == menu.parentId
        );
        this.selectedIcon = menu.icon;
        this.createForm(menu);
      });
  }

  createForm(menu?: IMenu) {
    this.menuForm = new FormGroup({
      displayText: new FormControl(menu?.displayText ?? null, [
        Validators.required,
      ]),
      url: new FormControl(menu?.url ?? null, []),
      orderNum: new FormControl(menu?.orderNum ?? null, [
        Validators.required,
        Validators.max(1000),
        Validators.min(0),
      ]),
    });

    this.setIconDropdownOptions(Object.values(ICON), this.selectedIcon);
    this.setParentDropdownOptions(this.parentMenus, this.selectedParentMenu);
  }

  setParentDropdownOptions(data: any[], selected?: any) {
    this.parentDropdownOptions = {
      items: data,
      onSelectionChange: (value) => {
        this.selectedParentMenu = value;
      },
      optionLabel: 'displayText',
      placeholder: 'Select',
      selected: selected,
    };
    this.parentDropdownOptions.errors?.next([]);
  }

  getMenuList(id?: number) {
    this.menuService
      .getAll()
      .pipe(takeUntil(this.onDestroy))
      .subscribe((response: any) => {
        this.parentMenus = response.body.data.filter(
          (m: any) => m.parentId == null
        );

        const defaultParent = {
          id: null,
          displayText: 'Parent Menu',
        };

        this.parentMenus.unshift(defaultParent);

        if (id) {
          this.getMenu(id);
        } else {
          this.setParentDropdownOptions(this.parentMenus, defaultParent);
        }
      });
  }

  setIconDropdownOptions(data: any, selected?: any) {
    if (selected == undefined) {
      selected = data[0];
      this.selectedIcon = selected;
    }

    this.iconDropdownOptions = {
      items: data,
      onSelectionChange: (value) => {
        this.selectedIcon = value;
      },
      optionLabel: data.key,
      placeholder: 'Select',
      selected: selected,
      icon:true
    };
    this.iconDropdownOptions.errors?.next([]);
  }

  save() {
    Object.keys(this.menuForm.controls).forEach((key) => {
      this.menuForm.get(key)?.markAsDirty();
    });

    if (this.menuForm.invalid) {
      return;
    }

    this.menuForm.disable();
    setSavingStatus(this.onInitializing, true);

    const menuItem = {
      displayText: this.menuItem.displayText,
      url: this.menuItem.url,
      orderNum: this.menuItem.orderNum,
      icon: this.selectedIcon,
      parentId: this.selectedParentMenu ? this.selectedParentMenu.id : null,
      menuGuid: Guid.create().toString(),
    } as IMenu;


    return this.menuService
      .add(menuItem)
      .pipe(takeUntil(this.onDestroy))
      .subscribe(
        (response) => {
          setSavingStatus(this.onInitializing, false);
          this.onResult.emit({ status: SidebarDialogResultStatus.saveSuccess });
        },
        (error) => {
          setSavingStatus(this.onInitializing, false);
          this.onResult.emit({ status: SidebarDialogResultStatus.saveFail });
          this.menuForm.enable();
        }
      );
  }

  update() {
    Object.keys(this.menuForm.controls).forEach((key) => {
      this.menuForm.get(key)?.markAsDirty();
    });

    if (this.menuForm.invalid) {
      return;
    }

    this.menuForm.disable();
    setUpdatingStatus(this.onInitializing, true);

    const menu = {
      ...this.selectedMenu,
      displayText: this.menuItem.displayText,
      url: this.menuItem.url,
      orderNum: this.menuItem.orderNum,
      icon: this.selectedIcon,
      parentId: this.selectedParentMenu ? this.selectedParentMenu.id : null,
    } as IMenu;

    if (this.initialData.id) {
      this.menuService
        .update(menu)
        .pipe(takeUntil(this.onDestroy))
        .subscribe(
          (response) => {
            this.onResult.emit({
              status: SidebarDialogResultStatus.updateSuccess,
            });
            setUpdatingStatus(this.onInitializing, false);
          },
          (error) => {
            this.onResult.emit({
              status: SidebarDialogResultStatus.updateFail,
            });
            this.menuForm.enable();
            setUpdatingStatus(this.onInitializing, false);
          }
        );
    }
  }

  get menuItem(): any {
    return {
      displayText: this.f.displayText.value,
      orderNum: this.f.orderNum.value,
      url: this.selectedParentMenu
        ? this.selectedParentMenu.id
          ? this.f.url.value
          : null
        : null,
    };
  }
}
