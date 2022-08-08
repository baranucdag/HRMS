import { IBaseModel } from './../base.model';

export interface IJobAdvert extends IBaseModel{
    positionName:string,
    qualificationLevel:string,
    workTimeType:string,
    workPlaceType:string,
    publishDate:Date,
    deadline:Date,
    description:string,
    status:boolean,
    isDeleted:number,
    isDeletedText:string
}