import { Observable } from 'rxjs';
import { ICandidate } from './../../models/views/candidate.model';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class CandidateService extends BaseService<ICandidate> {

  constructor(httpClient:HttpClient) {
    super(httpClient);
    this.apiPath = "candidates";
   }

   getByUserId(id:number):Observable<any>{
    return this.httpClient.get<any>(this.getFullPath("GetByUserId?id="+id))
   }

   updateWithCv(senform:any):Observable<HttpResponse<any>>{
    return this.httpClient.post<HttpResponse<any>>(super.getFullPath("addWithFile"),senform);
   }
}
