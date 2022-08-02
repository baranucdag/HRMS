import { HttpClient } from '@angular/common/http';
import { BaseService } from '.';
import { Injectable } from '@angular/core';
import { IJobAdvert } from '../../models/views/jobAdvert.model';

@Injectable({
  providedIn: 'root'
})
export class JobAdvertService extends BaseService<IJobAdvert> {

  constructor(httpClient:HttpClient) {
    super(httpClient);
    this.apiPath = "jobadverts";
   }
}
