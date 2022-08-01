import { IColumn, ITableGlobalFilterOptions, ITablePageOptions, ITableToolbarOptions, SortModeType } from ".";
import { columnColorOptions } from "../enums/table.enum";


export interface ITableOptions {
    data: any[],
    lazyLoad?: boolean;
    dataService?: any;
    dataServiceMethod?: any;
    columns: IColumn[];
    scrollable?: boolean;
    scrollHeight?: string | number;
    sortable?: boolean;
    sortMode?: SortModeType;
    filterable?: boolean;
    resizable?: boolean;
    reorderable?: boolean;
    selectable?: boolean;
    onSelectionChange?: (selectedRows: any[]) => any;
    height?: number;
    loading?: boolean;
    noRecordsTemplate?: string;
    pageOptions?: ITablePageOptions;
    globalFilterOptions?: ITableGlobalFilterOptions;
    toolbarOptions?: ITableToolbarOptions;
    showIndexColumn?: boolean;
    url?:string;
    columnColor?: columnColorOptions;
    isLogTable?:boolean;
}

