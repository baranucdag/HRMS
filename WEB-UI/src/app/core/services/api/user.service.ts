import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUser } from '../../models/views/user.model';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService<IUser> {

  constructor(httClient:HttpClient) {
    super(httClient)
    this.apiPath = "users";
   }

   //register a user with setting a claim
  updateWithClaim(sendForm: any): Observable<HttpResponse<IUser>> {
    return this.httpClient.post<HttpResponse<IUser>>(
      super.getFullPath('updateWithClaim'),
      sendForm
    );
  }
}
