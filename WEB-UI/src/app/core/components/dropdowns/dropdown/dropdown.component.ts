import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { IDropdownOptions } from './models';

@Component({
  selector: 'app-dropdown',
  templateUrl: './dropdown.component.html',
  styleUrls: ['./dropdown.component.scss'],
})
export class DropdownComponent implements OnInit {
  @Input() options!: IDropdownOptions;
  selectedOption: any;
  hasError = false;

  private readonly onDestroy = new Subject();
  constructor() {}

  ngOnInit(): void {
    this.options.errors = new Subject<string[]>();
    this.selectedOption = this.options.selected?.id;
    this.onError();
  }

  ngOnDestroy() {
    // @ts-ignore
    this.onDestroy.next();
    this.onDestroy.complete();
  }

  ngOnChanges(changes: SimpleChanges) {
    this.selectedOption = changes['options'].currentValue.selected?.id;
  }

  get optionLabel(): string {
    return this.options.optionLabel || '';
  }
  get optionValue(): string {
    return this.options.optionValue || '';
  }
  get optionDisabled(): string {
    return this.options.optionDisabled || '';
  }
  get placeholder(): string {
    return this.options.placeholder || 'Select';
  }
  get filter(): boolean {
    return this.options.filter || false;
  }
  get filterValue() {
    return this.options.filterValue || null;
  }
  get filterBy() {
    return this.options.filterBy || null;
  }
  get filterMatchMode(): string {
    return this.options.filterMatchMode || 'contains';
  }
  get filterPlaceholder() {
    return this.options.filterPlaceholder || null;
  }
  get showClear() {
    return this.options.showClear || false;
  }
  get resetFilterOnHide() {
    return this.options.resetFilterOnHide || true;
  }
  get label() {
    return this.options.label || '';
  }
  get selected() {
    return this.options.selected || undefined;
  }
  get disabled() {
    return this.options.disabled || false;
  }

  get icon() {
    return this.options.icon || false;
  }
  
  textCropper(text?: any) {
    text.toString();
    if (text.length > 32) {
      return text.substring(0, 31) + '...';
    } else {
      return text;
    }
  }

  onChange(event: any) {
    this.options.onSelectionChange(event.value);

    if (event.value) this.clearErrors();
  }

  onError() {
    this.options.errors!.pipe(takeUntil(this.onDestroy)).subscribe((errors) => {
      this.hasError = errors && errors.length > 0;
    });
  }

  clearErrors() {
    // @ts-ignore
    this.options.errors?.next();
  }

  readonly() {
    // @ts-ignore
    this.options.disabled = true;
  }
}
