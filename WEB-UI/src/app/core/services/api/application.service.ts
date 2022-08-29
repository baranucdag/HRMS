import { Observable } from 'rxjs';
import { IApplication } from './../../models/views/application.model';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { IResult } from '../../models';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ApplicationService extends BaseService<IApplication> {
  constructor(httpClient: HttpClient) {
    super(httpClient);
    this.apiPath = 'applications';
  }

  getByUserAndCandidateId(
    jobAdvertId: number,
    candidateId: number
  ): Observable<any> {
    return this.httpClient.get<any>(
      super.getFullPath('GetByUserIdAndCandidateId') +
        '?candidateId=' +
        candidateId +
        '&jobAdvertId=' +
        jobAdvertId
    );
  }

  getApplicationPaginationDataPath(url?:any) {
    if(url != undefined)
      return `${environment.apiUrl}${this.apiPath}/getapplicationspaginationdata/${url}`;

    return `${environment.apiUrl}${this.apiPath}/getapplicationspaginationdata`;
  }

  getApplicationsPeginationData(
    t: IApplication,
    url?: any
  ): Observable<HttpResponse<IResult<IApplication>>> {
    return this.httpClient.post<HttpResponse<IResult<IApplication>>>(
      super.getPaginationDataPath(url),
      t
    );
  }
}
