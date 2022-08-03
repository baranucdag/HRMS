import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ObserveType } from '../../enums/http';
import { IBaseModel, IResult } from '../../models';

@Injectable({
  providedIn: 'root'
})
export abstract class BaseService<T extends IBaseModel> {
  apiPath = "";
  params = {};
  headers = {};
  protected constructor(protected httpClient: HttpClient) {
  }

  get name() {
    return this.apiPath;
  }

  getFullPath(methodName: string = "") {
    return `${environment.apiUrl}${this.apiPath}/` + methodName;
  }

  getPaginationDataPath(url?:any) {
    if(url != undefined)
      return `${environment.apiUrl}${this.apiPath}/getpaginationdata/${url}`;

    return `${environment.apiUrl}${this.apiPath}/getpaginationdata`;
  }


  /**BASE METHODS */
  getPeginationData(t:T, url?:any) : Observable<HttpResponse<IResult<T>>>{
    return this.httpClient.post<IResult<T>>(this.getPaginationDataPath(url), t, { observe: ObserveType.response });
  }

  getAll(): Observable<HttpResponse<IResult<T[]>>> {
    return this.httpClient.get<IResult<T[]>>(this.getFullPath("getAll"), { observe: ObserveType.response });
  }

  getById(id: number): Observable<HttpResponse<IResult<T>>> {
    return this.httpClient.get<IResult<T>>(`${this.getFullPath()}${id}`, { observe: ObserveType.response });
  }

  add(t: T): Observable<HttpResponse<IResult<T>>> {
    return this.httpClient.post<IResult<T>>(this.getFullPath("add"), t, { observe: ObserveType.response });
  }

  update(t: T): Observable<HttpResponse<IResult<T>>> {
    return this.httpClient.post<IResult<T>>(`${this.getFullPath("update")}`, t, { observe: ObserveType.response });
  }

  delete(id: number): Observable<HttpResponse<IResult<T>>> {
    return this.httpClient.post<IResult<T>>(`${this.getFullPath("delete?id=")}${id}`,id,{ observe: ObserveType.response });
  }

}
