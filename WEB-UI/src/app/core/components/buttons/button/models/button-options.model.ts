export interface IButtonOptions {
    label?: string;
    class?: string;
    style?: string;
    icon?: string;
    iconPosition?: 'left' | 'right' | 'top' | 'bottom';
    tooltip?: string;
    loading?: boolean;
    disabled?: boolean;
    hidden?:boolean;
    properties?: any[];
}

export interface IButtonEventsOptions {
    options: IButtonOptions;
    onClick: (event?: any) => any;
}   