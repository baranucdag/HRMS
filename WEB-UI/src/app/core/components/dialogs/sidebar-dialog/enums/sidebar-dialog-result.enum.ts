import { SidebarDialogResultStatus } from "./sidebar-dialog-result-status.enum";

export interface SidebarDialogResult{
    status: SidebarDialogResultStatus;
    data?: any;
}