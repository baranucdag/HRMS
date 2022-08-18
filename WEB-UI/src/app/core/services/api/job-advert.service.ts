import { IResult } from './../../models/result.model';
import { Observable } from 'rxjs';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { BaseService } from '.';
import { Injectable } from '@angular/core';
import { IJobAdvert } from '../../models/views/jobAdvert.model';
import { IQueryObject } from '../../models/views/queryObject.model';

@Injectable({
  providedIn: 'root',
})
export class JobAdvertService extends BaseService<IJobAdvert> {
  constructor(httpClient: HttpClient) {
    super(httpClient);
    this.apiPath = 'jobadverts';
  }

  getAllPaged(
    query: IQueryObject
  ): Observable<any> {
    return this.httpClient.get<any>(
      super.getFullPath('getallpaged') +
        '?QueryString=' +
        query.queryString +
        '&PageSize=' +
        query.pageSize +
        '&PageNumber=' +
        query.pageNumber +
        '&totalCount=' +
        query.totalCount
    );
  }
}
