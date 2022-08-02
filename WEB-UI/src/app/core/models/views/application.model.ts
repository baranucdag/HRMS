import { IBaseModel } from '..';
export interface IApplication extends IBaseModel{
    jobAdvertId:number,
    candidateId:number,
    applicationDate:Date,
    isDeleted:boolean
}