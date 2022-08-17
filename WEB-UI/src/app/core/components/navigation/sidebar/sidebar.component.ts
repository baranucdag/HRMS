import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { Subject, takeUntil } from 'rxjs';
import { IMenu } from 'src/app/core/models/views';
import { MenuService } from 'src/app/core/services/api/menu.service';
import { SidebarService } from 'src/app/core/services/navigation/sidebar.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
})
export class SidebarComponent implements OnInit {
  private readonly onDestroy = new Subject<void>();

  selectedMenuId?: number;
  activeMenuId?: number;
  parentMenuItems: IMenu[] = [];
  allMenuItems: IMenu[] = [];
  childMenuItems: IMenu[] = [];

  loadingStates = {
    menuList: true,
  };

  constructor(
    private menuService: MenuService,
    public sidebarService: SidebarService,
    private router: Router
  ) {
    this.getMenuList();
  }

  ngOnInit(): void {}

  getMenuList(): void {
    /*this.menuService
      .getAll()
      .pipe(takeUntil(this.onDestroy))
      .subscribe((result) => {
        if (result.body?.data) {
          this.allMenuItems = result.body.data;
          this.parentMenuItems = this.allMenuItems.filter(
            (menu) => menu.parentId == null
          );
           let url = this.router.url.substring(1);
           const activeMenu = this.allMenuItems.find(x=>x.url===url);
           this.activeMenuId=activeMenu?.parentId;
        }

        setTimeout(() => {
          this.loadingStates.menuList=false;
        },100);
      }); */

    /* statik menü*/
    this.allMenuItems = [
      {
        id: 1,
        displayText: 'Menu Management',
        icon: 'pi pi-list',
        orderNum: 10,
        menuGuid: 'asdasdasdasdasd',
      },
      {
        id: 2,
        displayText: 'Menu List',
        icon: 'bi bi-table',
        parentId: 1,
        orderNum: 11,
        url: 'admin/menu-management/menu-list',
        menuGuid: 'assadasdasdadsdasdasdasdasd',
      },
      {
        id: 3,
        displayText: 'Job Advert Management',
        icon: 'pi pi-list',
        orderNum: 11,
        url: 'jobadvert',
        menuGuid: 'asdasdasdasdasda',
      },
      {
        id: 4,
        displayText: 'Job Advert List',
        icon: 'bi bi-table',
        parentId: 3,
        orderNum: 11,
        url: 'admin/jobadvert/jobadvert-list',
        menuGuid: 'assadasdasdadsdasdasdasdasd',
      },
      {
        id: 5,
        displayText: 'Candidate Management',
        icon: 'pi pi-list',
        orderNum: 11,
        url: 'canidate',
        menuGuid: 'asdasdasdasdasda',
      },
      {
        id: 6,
        displayText: 'Candidate List',
        icon: 'bi bi-table',
        parentId: 5,
        orderNum: 11,
        url: 'admin/candidate/candidate-list',
        menuGuid: 'assadasdasdadsdasdasdasdasd',
      },
      {
        id: 7,
        displayText: 'Petition Management',
        icon: 'pi pi-list',
        orderNum: 11,
        url: 'application',
        menuGuid: 'asdasdasdasdasda',
      },
      {
        id: 8,
        displayText: 'Petition List',
        icon: 'bi bi-table',
        parentId: 7,
        orderNum: 11,
        url: 'admin/application/application-list',
        menuGuid: 'assadasdasdadsdasdasdasdasd',
      },
      {
        id: 9,
        displayText: 'User Management',
        icon: 'pi pi-list',
        orderNum: 11,
        url: 'user',
        menuGuid: 'asdasdasdasdasda',
      },
      {
        id: 10,
        displayText: 'User List',
        icon: 'bi bi-table',
        parentId: 9,
        orderNum: 11,
        url: 'admin/user/user-list',
        menuGuid: 'assadasdasdadsdasdasdasdasd',
      },
    
    ] as IMenu[];

    this.parentMenuItems = this.allMenuItems.filter(
      (menu) => menu.parentId == null
    );

    let url = this.router.url.substring(1);
    const activeMenu = this.allMenuItems.find((x) => x.url === url);
    this.activeMenuId = activeMenu?.parentId;

    setTimeout(() => {
      this.loadingStates.menuList = false;
    }, 100);
    /**/
  }

  selectedParentMenu(id: number) {
    if (this.selectedMenuId == id) {
      this.selectedMenuId = undefined;
    } else {
      this.selectedMenuId = id;
      this.childMenuItems = this.allMenuItems.filter(
        (menu) => menu.parentId == id
      );
    }
  }

  activeMenu(menu: any) {
    this.activeMenuId = menu.parentId;
  }

  removeSelect() {
    this.activeMenuId = undefined;
    this.selectedMenuId = undefined;
  }
}
