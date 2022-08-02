import { ICandidate } from './../../models/views/candidate.model';
import { HttpClient } from '@angular/common/http';
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
}
