import { DecodedToken } from './../../models/dtos/decoded.token.model';
import { LocalStorageService } from './../local-storage/local-storage.service';
import { IRegister } from './../../models/views/register.model';
import { ILogin } from './../../models/views/login.model';
import { Observable } from 'rxjs';
import { IUser } from './../../models/views/user.model';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BaseService } from '.';
import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})

export class AuthService extends BaseService<IUser> {
  jwthelperService: JwtHelperService = new JwtHelperService();
  decodedToken: DecodedToken = {Token: "", DecodedToken: "", Expiration: 0, Email: "", Name: "", Role: "", Roles: [], UserId: 0};

  constructor(
    httpClient: HttpClient,
    private localStorageService: LocalStorageService
  ) {
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
  login(senModel: ILogin): Observable<any> {
    return this.httpClient.post<any>(super.getFullPath('login'), senModel);
  }

  //register
  register(senModel: IRegister): Observable<any> {
    return this.httpClient.post<any>(
      super.getFullPath('register'),
      senModel
    );
  }

  //check if user is authenticated
  isAuthenticated() {
    if (localStorage.getItem('token')) {
      return true;
    } else {
      return false;
    }
  }


  //check if token expired
  isTokenExpired(): boolean {
    if (new Date(this.decodedToken['Expiration'] * 1000) < new Date()  || this.decodedToken['Expiration'] == undefined
      || this.decodedToken['Expiration'] == null) {
      return true;
      this.localStorageService.Remove('token');
    } else {
      return false
    }
  }
  

  //get user details by decoding token
  getUserDetailsFromToken() {
    const token: any = this.localStorageService.get('token');
    const decodedToken = this.jwthelperService.decodeToken(token);
    this.decodedToken['Token'] = this.localStorageService.get('token');
    this.decodedToken['DecodedToken'] =
      this.jwthelperService.decodeToken(token);
    this.decodedToken['Expiration'] = +decodedToken['exp'];
    this.decodedToken['Name'] =
      decodedToken[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'
      ];
    this.decodedToken['Role'] =
      decodedToken[
        'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
      ];
    this.decodedToken['Roles'] =
      decodedToken[
        'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
      ];
    this.decodedToken['Email'] = decodedToken['email'];
    this.decodedToken['UserId'] = parseInt(
      decodedToken[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
      ]
    );
  }
}
