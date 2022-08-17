import { BaseService } from '.';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IUserOperationClaim } from '../../models/views/userOperationClaim.model';

@Injectable({
  providedIn: 'root'
})
export class UserOperationClaimService extends BaseService<IUserOperationClaim> {

  constructor(httpClient:HttpClient) {
    super(httpClient);
    this.apiPath = "userOperationClaims"
   }
}
