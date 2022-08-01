import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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
  activeMenuId?:number;
  parentMenuItems: IMenu[] = [];
  allMenuItems: IMenu[] = [];
  childMenuItems: IMenu[] = [];

  loadingStates = {
    menuList:true
  };

  constructor(
    private menuService: MenuService, 
    public sidebarService: SidebarService,
    private router:Router
    ) {
   this.getMenuList();
  }

  ngOnInit(): void {}

  getMenuList(): void {
    this.menuService
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
      });
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

  activeMenu(menu:any){
    this.activeMenuId = menu.parentId;
  }

  removeSelect(){
    this.activeMenuId=undefined;
    this.selectedMenuId = undefined;
  }

}
