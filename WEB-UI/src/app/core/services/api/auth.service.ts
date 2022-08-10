import { Observable } from 'rxjs';
import { IUser } from './../../models/views/user.model';
import { BaseService } from '.';
import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { IResult } from '../../models';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService extends BaseService<IUser> {
  constructor(httpClient: HttpClient) {
    super(httpClient);
    this.apiPath = 'auth';
  }

  //register a user with setting a claim
  registerWithClaim(sendForm:any):Observable<HttpResponse<IUser>>{
    return this.httpClient.post<HttpResponse<IUser>>(super.getFullPath("RegisterWithClaim"),sendForm);
  }
}
