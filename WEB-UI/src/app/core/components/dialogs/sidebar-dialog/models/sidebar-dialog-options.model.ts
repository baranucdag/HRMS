import { Position } from "src/app/core/types";
import { SidebarDialogResult, SidebarDialogSize } from "../enums";

export interface ISidebarDialogOptions {
    title?: string;
    position?: Position;
    size?: SidebarDialogSize;
    dismissible?: boolean;
    fullScreen?:boolean;
    onResult: (result: SidebarDialogResult) => any;
    buttons?: ISidebarDialogOptionButtons;
    data?: any;
}

export interface ISidebarDialogOptionButtons {
    save?: boolean;
    update?: boolean;
    cancel?: boolean;
    next?: boolean;
    previous?: boolean;
}
