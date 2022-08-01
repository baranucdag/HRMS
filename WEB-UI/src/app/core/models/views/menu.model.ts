import { IBaseModel } from "..";

export interface IMenu extends IBaseModel {
    displayText: string;
    icon: string;
    url?: string;
    parentId?:number;
    orderNum?:number;
    menuGuid:string;
}