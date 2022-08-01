import { ColumnDataType, ColumnFilterDataType, ColumnFilterMatchMode, ColumnSortDirection } from ".";
import { IButtonOptions } from "../../../buttons/button/models";



export interface IColumn {
    title: string;
    type: ColumnDataType;
    field?: string;
    format?: string;
    width?: string | number;
    filter?: IColumnFilterOptions;
    frozen?: boolean;
    hidden?: boolean;
    sortable?: boolean;
    filterable?: boolean;
    template?: ((row: any, index: number) => string) | string;
    buttons?: IColumnButton[];
    switchButton?:ISwitchButton;
}

export interface IColumnFilterOptions {
    matchMode?: ColumnFilterMatchMode;
    data?: IColumnFilterData[];
    type?: ColumnFilterDataType;
    defaultValue?: IColumnFilterData;
}

export interface IColumnFilterData {
    label: string;
    value: any;
}

export interface IColumnButton {
    options: IButtonOptions;
    hidden?: (row: any, index: number) => boolean;
    onClick: (row: any, index: number) => Function;
}

export interface IColumnSort {
    field: string;
    direction: ColumnSortDirection;
}

export interface ISwitchButton{
    value:any,
    service:any,
    disabled:boolean
}