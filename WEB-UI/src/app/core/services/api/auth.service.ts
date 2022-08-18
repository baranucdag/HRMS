import { IRegister } from './../../models/views/register.model';
import { ILogin } from './../../models/views/login.model';
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
  registerWithClaim(sendForm: any): Observable<HttpResponse<any>> {
    
    return this.httpClient.post<HttpResponse<any>>(
      super.getFullPath('RegisterWithClaim'),
      sendForm
    );
  }

  //login
  login(senModel:ILogin): Observable<HttpResponse<ILogin>> {
    return this.httpClient.post<HttpResponse<ILogin>>(
      super.getFullPath('login'),
      senModel
    );
  }

  //register
  register(senModel:IRegister): Observable<HttpResponse<IRegister>> {
    return this.httpClient.post<HttpResponse<IRegister>>(
      super.getFullPath('register'),
      senModel
    );
  }
}
