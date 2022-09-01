import { IBaseModel } from '..';
export interface IApplication extends IBaseModel{
    id:number
    jobAdvertId:number,
    candidateId:number,
    applicationStatus:number,
    prevApplicationStatus:number,
    hasEmailSent:boolean,
    applicationDate:Date,
    isDeleted:number
}