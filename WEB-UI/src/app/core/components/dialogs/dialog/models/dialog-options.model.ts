import { IButtonEventsOptions } from "../../../buttons/button/models";
import { DialogSize, DialogType } from "../enums";

export interface IDialogOptions {
    title?: string;
    message?: string;
    type?: DialogType;
    size?: DialogSize
    onConfirm?: () => any;
    onReject?: () => any;
    onCancel?: () => any;
    customButtons?: IButtonEventsOptions[]
}