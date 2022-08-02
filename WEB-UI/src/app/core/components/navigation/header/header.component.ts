import { Component, OnInit, ViewChild } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { LANGUAGES, LANGUAGE_LIST } from 'src/app/core/constants';
import { ILanguage } from 'src/app/core/models/i18n';
import { I18nService } from 'src/app/core/services/i18n';
import { T } from 'src/app/core/helpers/i18n';
import { ICON } from 'src/app/core/constants/icons/icon';
import * as _ from 'lodash';
import { SidebarService } from 'src/app/core/services/navigation/sidebar.service';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import {
  SidebarDialogResult,
  SidebarDialogResultStatus,
  SidebarDialogSize,
} from '../../dialogs/sidebar-dialog/enums';
import { Subject } from 'rxjs';
import { DialogService } from 'src/app/core/services/dialog';
import { DialogComponent } from '../../dialogs';
import { IDialogOptions } from '../../dialogs/dialog/models';
import { DialogSize, DialogType } from '../../dialogs/dialog/enums';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  selectedLanguage: any;
  menuItems?: MenuItem[];
  showBool: boolean = false;
  userDisplayName?: string;
  @ViewChild('logOutDialog') logOutDialog?: DialogComponent;
  logOutDialogOptions!: IDialogOptions;

  private readonly onDestroy = new Subject<void>();

  constructor(
    private i18nService: I18nService,
    private sidebarService: SidebarService,
    private router: Router,
    private readonly dialogService: DialogService
  ) {}

  ngOnInit(): void {
    this.selectedLanguage = LANGUAGE_LIST.find((l) => l.code === this.i18nService.language);
    //this.createLogOutDialogOptions();
    this.initMenu();
  }

  initMenu() {
    const languages = this.languages.map((m: ILanguage) => {
      return {
        label: T(m.name),
        command: (e) => {
          this.initMenu();
          this.onLanguageChange(e.item.label);
        },
      } as MenuItem;
    });

    this.menuItems = [
      {
        icon: ICON.translate,
        items: languages
      }
    ];
  }

  get languages() {
    return LANGUAGE_LIST;
  }

  onLanguageChange(language: any) {
    const code = this.languages.find(m => _.isEqual(m.name, language))?.code || LANGUAGES.tr;
    // console.log(code);
    this.i18nService.changeLanguage(code);
  }

  toggleSidebar() {
    this.sidebarService.showSidebar = !this.sidebarService.showSidebar;
  }

  logout() {
    //this.authService.logout();
  }

  hideMenuItems() {
    this.showBool = !this.showBool;
  }

  createLogOutDialogOptions() {
    this.logOutDialogOptions = {
      title: 'Log Out',
      message: 'Are you sure you want to log out?',
      type: DialogType.confirm,
      size: DialogSize.small,
      onConfirm: () => {
        this.logout();
      },
    };
  }
}
