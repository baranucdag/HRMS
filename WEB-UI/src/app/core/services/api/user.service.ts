import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
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
}
