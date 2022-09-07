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

  getByJobAdvertId(
    id: number
  ): Observable<any> {
    return this.httpClient.get<any>(
      super.getFullPath('GetByJobAdvertId') +
        '?jobAdvertId=' +
        id
    );
  }

  getByCandidateId(
    id: number
  ): Observable<any> {
    return this.httpClient.get<any>(
      super.getFullPath('getByCandidateId') +
        '?id=' +
        id
    );
  }
}
