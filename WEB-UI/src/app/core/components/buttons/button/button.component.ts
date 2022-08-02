import {
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { IButtonOptions } from './models';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss'],
})
export class ButtonComponent implements OnInit {
  @Input() options!: IButtonOptions;
  @Output() onClick = new EventEmitter<any>();
  constructor() {}

  ngOnInit(): void {
  }

  onButtonClick(event: any) {
    this.onClick.emit(event);
  }

  get label() {
    return this.options.label || '';
  }
  get class() {
    return (this.options.class || '') + ' ' + this.properties;
  }
  get properties() {
    return this.options.properties
      ? this.options.properties!.map((p) => `${p.toString()} `).join(' ')
      : '';
  }
  get style() {
    return this.options.style || '';
  }
  get iconClass() {
    return this.options.icon || '';
  }
  get iconPosition() {
    return this.options.iconPosition! || 'left';
  }
  get tooltip() {
    return this.options.tooltip || '';
  }
  get loading() {
    return this.options.loading || false;
  }
  get disabled() {
    return this.options.disabled || false;
  }
  get hidden() {
    return this.options.hidden || false;
  }
}
