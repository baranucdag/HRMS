import { IBaseModel } from './../base.model';

export interface IJobAdvert extends IBaseModel{
    id:number,
    positionName:string,
    qualificationLevel:string,
    department:string,
    workTimeType:string,
    workPlaceType:string,
    publishDate:Date,
    deadline:Date,
    description:string,
    status:boolean,
    isDeleted:number,
    isDeletedText:string
    createdAt:Date
}