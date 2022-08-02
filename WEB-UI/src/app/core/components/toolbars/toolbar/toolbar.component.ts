import { Component, Input, OnInit } from '@angular/core';
import * as _ from 'lodash';

import { Subject, takeUntil } from 'rxjs';
import { ICON } from 'src/app/core/constants';
import { ButtonColor, ButtonSize, ButtonType } from '../../buttons/button/enums';
import { IButtonEventsOptions } from '../../buttons/button/models';
import { IToolbarOptions } from './models/toolbar-options.model';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss'],
})
export class ToolbarComponent implements OnInit {
  newButton?: IButtonEventsOptions;
  deleteButton?: IButtonEventsOptions;
  updateButton?: IButtonEventsOptions;
  backButton?: IButtonEventsOptions;
  saveButton?: IButtonEventsOptions;
  cancelButton?: IButtonEventsOptions;

  @Input() options!: IToolbarOptions;
  private readonly onDestroy = new Subject();
  get panelClass() {
    return this.options?.panelClass || '';
  }
  get panelStyle() {
    return this.options?.panelStyle || '';
  }
  get leftGroups() {
    return (
      this.options?.buttonGroups?.filter(
        (m) => !m.position || m.position === 'left'
      ) || []
    );
  }
  get rightGroups() {
    return (
      this.options?.buttonGroups?.filter((m) => m.position === 'right') || []
    );
  }
  get defaultButtonPositionIsLight(): boolean {
    return (
      this.options?.defaultButtons?.position !== undefined &&
      this.options?.defaultButtons?.position === 'left'
    );
  }
  constructor() { }

  ngOnChanges(): void {
    if (this.options.defaultButtons?.new) {
      this.addNewButton(this.options.defaultButtons?.new);
    }

    if (this.options.defaultButtons?.delete) {
      this.addDeleteButton(this.options.defaultButtons?.delete);
    }
  }

  ngOnInit(): void {
    if (this.options.defaultButtons?.new) {
      this.addNewButton(this.options.defaultButtons?.new);
    }

    if (this.options.defaultButtons?.delete) {
      this.addDeleteButton(this.options.defaultButtons?.delete);
    }

    if (this.options.defaultButtons?.save) {
      this.addSaveButton(this.options.defaultButtons?.save);
    }

    if (this.options.defaultButtons?.update) {
      this.addUpdateButton(this.options.defaultButtons.update)
    }

    if (this.options.defaultButtons?.back) {
      this.addBackButton(this.options.defaultButtons.back)
    }

    if (this.options.defaultButtons?.cancel) {
      this.addCancelButton(this.options.defaultButtons.cancel)
    }
  }

  click(event: any, button: any) {
    button.onClick(event);
  }

  addNewButton(buttonOptions: any) {
    this.newButton = {
      options: {
        label: 'New',
        properties: [ButtonSize.small, ButtonColor.success],
        icon: ICON.plusCircle,
        hidden: buttonOptions.options?.hidden,
      },
      onClick: buttonOptions.onClick,
    };
  }

  addDeleteButton(buttonOptions: any) {
    this.deleteButton = {
      options: {
        label: 'Delete',
        properties: [ButtonSize.small, ButtonColor.danger],
        icon: ICON.trash,
        hidden: this.options?.defaultButtons?.delete?.options?.hidden,
      },
      onClick: buttonOptions.onClick,
    };
  }

  addSaveButton(buttonOptions: any) {
    this.saveButton = {
      options: {
        label: 'Save',
        properties: [ButtonSize.small, ButtonColor.success],
        icon: ICON.save,
        hidden: this.options?.defaultButtons?.delete?.options?.hidden,
      },
      onClick: buttonOptions.onClick,
    };

    if (this.options.defaultButtons?.save?.onLoading) {
      this.options.defaultButtons.save.onLoading
        .pipe(takeUntil(this.onDestroy))
        .subscribe((value) => {
          if (!_.isUndefined(value))
            this.saveButton!.options.loading = value;
        });
    }
  }

  addCancelButton(buttonOptions: any) {
    this.cancelButton = {
      options: {
        label: 'Cancel',
        properties: [ButtonSize.small, ButtonColor.transparent],
        icon: ICON.times,
      },
      onClick: buttonOptions.onClick,
    };
  }

  addBackButton(buttonOptions: any) {
    this.backButton = {
      options: {
        label: 'Back',
        properties: [ButtonSize.small, ButtonColor.secondary],
        icon: ICON.arrowLeftCircle,
      },
      onClick: buttonOptions.onClick,
    };
  }

  addUpdateButton(buttonOptions: any) {
    this.updateButton = {
      options: {
        label: 'Update',
        properties: [ButtonSize.small, ButtonColor.success],
        icon: ICON.pencil,
      },
      onClick: buttonOptions.onClick,
    };

    if (this.options.defaultButtons?.update?.onLoading) {
      this.options.defaultButtons.update.onLoading
        .pipe(takeUntil(this.onDestroy))
        .subscribe((value) => {
          if (!_.isUndefined(value))
            this.updateButton!.options.loading = value;
        });
    }
  }
}
