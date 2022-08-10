import { IBaseModel } from "../base.model";

export interface IUser extends IBaseModel{
    firstName:string,
    lastName:string,
    password:string,
    userClaim:string,
    email:string,
    status:boolean
}