import { Subject } from "rxjs";

export interface IDropdownOptions {
    items: any[];
    optionLabel: string;
    label?: string;
    floatingLabel?: boolean;
    optionValue?: string;
    optionDisabled?: string;
    selected?: any;
    placeholder?: string;
    filter?: boolean;
    filterValue?: string;
    filterBy?: string; // When filtering is enabled, filterBy decides which field or fields (comma separated) to search against.
    filterMatchMode?: string; // Defines how the items are filtered, valid values are "contains" (default) "startsWith", "endsWith", "equals", "notEquals", "in", "lt", "lte", "gt" and "gte".
    filterPlaceholder?: string;
    showClear?: boolean;
    resetFilterOnHide?: boolean; // default true
    onSelectionChange: (event: any) => any;
    errors?: Subject<string[]>;
    disabled?: boolean;
    icon?:boolean;
}