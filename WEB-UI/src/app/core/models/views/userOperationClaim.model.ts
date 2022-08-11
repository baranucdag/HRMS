import { IBaseModel } from './../base.model';
export interface IUserOperationClaim extends IBaseModel{
    userId:number,
    operationClaimId:number
}