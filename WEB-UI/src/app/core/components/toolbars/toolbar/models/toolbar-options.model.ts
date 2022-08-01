import { Subject } from 'rxjs';
import { IButtonEventsOptions } from '../../../buttons/button/models';

export interface IToolbarOptions {
  panelClass?: string;
  panelStyle?: string;
  defaultButtons?: IToolbarDefaultButtons;
  buttonGroups?: IToolbarButtonGroupOptions[];
}

export interface IToolbarButtonGroupOptions {
  position?: 'left' | 'right';
  buttons: IButtonEventsOptions[];
}

export interface IToolbarDefaultButtons {
  position?: 'left' | 'right';
  new?: IToolbarDefaultButtonOptions;
  delete?: IToolbarDefaultButtonOptions;
  back?: IToolbarDefaultButtonOptions;
  update?: IToolbarDefaultButtonOptions;
  save?: IToolbarDefaultButtonOptions;
  cancel?: IToolbarDefaultButtonOptions;
  trash?: IToolbarDefaultButtonOptions;
  untrash?: IToolbarDefaultButtonOptions;
  undelete?: IToolbarDefaultButtonOptions;
  checkExpireDates?: IToolbarDefaultButtonOptions;
}

export interface IToolbarDefaultButtonOptions {
  onClick: (event?: any) => any;
  onLoading?: Subject<boolean>;
  onDisabling?: Subject<boolean>;
  options?: {
    hidden?: boolean;
  };
}
