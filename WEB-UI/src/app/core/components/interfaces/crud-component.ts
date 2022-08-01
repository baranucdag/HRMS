import { EventEmitter } from "@angular/core";
import { Subject } from "rxjs";
import { SidebarDialogResult } from "../dialogs/sidebar-dialog/enums";

export interface ICrudComponent {
    save?: () => any;
    update?: () => any;
    cancel?: () => any;
    setData: (data: any) => any;
    onResult: EventEmitter<SidebarDialogResult>;
    onInitializing?: Subject<any>;
}