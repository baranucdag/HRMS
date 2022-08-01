import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SidebarService {
  constructor() { }

  private _showSidebar = true;
  public get showSidebar(): boolean {
    return this._showSidebar;
  }
  public set showSidebar(v: boolean) {
    this._showSidebar = v;
  }
}
