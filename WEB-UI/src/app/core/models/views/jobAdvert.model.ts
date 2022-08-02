import { IBaseModel } from './../base.model';

export interface IJobAdvert extends IBaseModel{
    positionName:string,
    qualificationLevel:string,
    workType:string,
    publishDate:Date,
    deadline:Date,
    status:boolean,
    isDeleted:boolean,
    
}