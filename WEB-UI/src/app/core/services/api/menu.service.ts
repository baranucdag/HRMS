import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from '.';
import { IMenu } from '../../models/views';

@Injectable({
  providedIn: 'root'
})
export class MenuService extends BaseService<IMenu> {

  constructor(httpClient: HttpClient) {
    super(httpClient);
    this.apiPath = "menus";
  }
}
