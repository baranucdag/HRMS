import { IApplication } from './../../models/views/application.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService extends BaseService<IApplication> {

  constructor(httpClient:HttpClient) {
    super(httpClient);
    this.apiPath = "applications";
   }
}
