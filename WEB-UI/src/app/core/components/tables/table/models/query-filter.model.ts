export interface IQueryFilterModel{
    [x: string]: any;
    date: IMatchModeModel;
    string: IMatchModeModel;
    numeric: IMatchModeModel;
    enum: IMatchModeModel;
}

export interface IMatchModeModel{
    [x: string]: any;
    eq?: string;
    contains?: string;
}
