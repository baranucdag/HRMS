import { IBaseModel } from "./base.model";

export interface IResult<T extends IBaseModel | any>{
  statusCode: string;
  message: string;
  isOk: boolean;
  data: T;
  processCode:string;
}
