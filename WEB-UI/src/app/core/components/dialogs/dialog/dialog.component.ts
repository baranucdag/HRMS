import {Component, Input, OnInit} from '@angular/core';
import {ICON} from 'src/app/core/constants';
import {ButtonColor, ButtonSize, ButtonType} from '../../buttons/button/enums';
import {IButtonEventsOptions} from '../../buttons/button/models';
import {DialogSize, DialogType} from './enums';
import {IDialogOptions, IDialogSize} from './models';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.scss']
})
export class DialogComponent implements OnInit {
  display: boolean = false;
  @Input() options!: IDialogOptions;
  cancelButton?: IButtonEventsOptions;
  confirmButton?: IButtonEventsOptions;
  secondConfirmButton?: IButtonEventsOptions;
  rejectButton?: IButtonEventsOptions;
  defaultSize!: IDialogSize; //mediumSize
  largeSize!: IDialogSize;
  smallSize!: IDialogSize;

  constructor() {
    this.close = this.close.bind(this);
    this.confirm = this.confirm.bind(this);
    this.reject = this.reject.bind(this);
    this.firstConfirm = this.firstConfirm.bind(this);

    this.defaultSize = {
      breakpoints: {'960px': '75vw', '640px': '100vw'},
      style: {width: '50vw', 'min-width': '20vw'}
    }

    this.largeSize = {
      breakpoints: {'960px': '100vw', '640px': '125vw'},
      style: {width: '75vw', 'min-width': '20vw'}
    }

    this.smallSize = {
      breakpoints: {'960px': '50vw', '640px': '75vw'},
      style: {width: '25vw', 'min-width': '20vw'}
    }
  }

  ngOnInit(): void {
    this.setOptions();
  }

  ngOnChanges(changeEvent: any) {
    this.options = changeEvent.options.currentValue
    this.setOptions();
  }

  setOptions() {
    if (this.isType(this.dialogTypes.confirm)) {
      this.confirmButton = {
        onClick: this.confirm,
        options: {
          label: "Accept",
          properties: [ButtonSize.small, ButtonType.raised, ButtonColor.success],
          icon: ICON.check
        }
      }
      this.rejectButton = {
        onClick: this.reject,
        options: {
          label: "Cancel",
          properties: [ButtonSize.small, ButtonType.raised, ButtonColor.danger],
          icon: ICON.times
        }
      }
    } else if (this.isType(this.dialogTypes.doubleConfirm)) {
      this.confirmButton = {
        onClick: this.firstConfirm,
        options: {
          label: "Accept",
          properties: [ButtonSize.small, ButtonType.raised, ButtonColor.success],
          icon: ICON.check,
        }
      }

      this.rejectButton = {
        onClick: this.reject,
        options: {
          label: "Cancel",
          properties: [ButtonSize.small, ButtonType.raised, ButtonColor.danger],
          icon: ICON.times,
        }
      }
    }
  }


  open() {
    this.display = true;
  }

  close() {
    this.display = false;
    if (this.options.onCancel)
      this.options.onCancel();

    this.setSecondConfirmButtonHiddenTrue();
  }

  confirm() {
    if (this.options.onConfirm)
      this.options.onConfirm();

    this.close();
  }

  firstConfirm() {
    this.close();

    this.secondConfirmButton = {
      onClick: this.confirm,
      options: {
        label: "Accept",
        properties: [ButtonSize.small, ButtonType.raised, ButtonColor.success],
        icon: ICON.check,
      }
    }
    this.confirmButton = {
      onClick: this.firstConfirm,
      options: {
        label: "Accept",
        properties: [ButtonSize.small, ButtonType.raised, ButtonColor.success],
        icon: ICON.check,
        hidden: true
      }
    }

    this.open();
  }

  reject() {
    if (this.options.onReject)
      this.options.onReject();

    this.close();
  }

  isType(type: DialogType) {
    return type === this.options.type;
  }

  get dialogTypes() {
    return DialogType;
  }

  get title() {
    return this.options.title;
  }

  get message() {
    return this.options.message;
  }

  get size() {
    if (!this.options || !this.options.size)
      return this.defaultSize;

    switch (this.options.size) {
      case DialogSize.small:
        return this.smallSize;
      case DialogSize.large:
        return this.largeSize;
      case DialogSize.medium:
        return this.defaultSize;
    }
  }

  setSecondConfirmButtonHiddenTrue() {
    this.secondConfirmButton = {
      onClick: this.confirm,
      options: {
        label: "Accept",
        properties: [ButtonSize.small, ButtonType.raised, ButtonColor.success],
        icon: ICON.check,
        hidden: true
      }
    }
  }

}
