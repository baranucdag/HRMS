import { IJobAdvert } from 'src/app/core/models/views/jobAdvert.model';
export interface IQueryObject{
    queryString:string,
    pageSize:number,
    pageNumber:number,
    totalCount:number,
    items:IJobAdvert[]
}