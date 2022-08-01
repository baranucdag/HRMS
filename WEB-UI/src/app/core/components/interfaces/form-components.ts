import { EventEmitter } from "@angular/core";
import { SidebarDialogResult } from "../dialogs/sidebar-dialog/enums";

export interface IFormComponent {
    initialData:any;
    onResult: EventEmitter<SidebarDialogResult>;
    setData: (params: any) => any;
    save?: () => any;
    update?: () => any;
}