import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  ComponentFactoryResolver,
  ComponentRef,
  ElementRef,
  HostListener,
  OnDestroy,
  Renderer2,
  Type,
  ViewChild,
} from '@angular/core';
import {Subject} from 'rxjs';
import {InsertionDirective} from 'src/app/core/directives';
import {
  SidebarDialogResult,
  SidebarDialogResultStatus,
  SidebarDialogSize,
} from './enums';
import {ISidebarDialogOptions} from './models';
import {Position} from './../../../types';
import {T} from 'src/app/core/helpers/i18n';
import {IToolbarOptions} from '../../toolbars/toolbar/models';
import {takeUntil} from 'rxjs/operators';
import {ICrudComponent} from '../../interfaces';

@Component({
  templateUrl: './sidebar-dialog.component.html',
  styleUrls: ['./sidebar-dialog.component.scss'],
})
export class SidebarDialogComponent implements AfterViewInit, OnDestroy {
  options?: ISidebarDialogOptions;
  display = false;
  componentRef?: ComponentRef<ICrudComponent>;
  childComponentType?: Type<any>;
  @ViewChild(InsertionDirective) insertionPoint!: InsertionDirective;
  @ViewChild('footer') footer!: ElementRef<any>;
  private readonly _onClose = new Subject<any>();
  public onClose = this._onClose.asObservable();
  private readonly onDestroy = new Subject();
  private wasInside = true;
  private composedPathLength: number = 5;

  toolbarOptions?: IToolbarOptions;

  constructor(
    private componentFactoryResolver: ComponentFactoryResolver,
    private cd: ChangeDetectorRef,
    private renderer2: Renderer2
  ) {
  }

  @HostListener('click')
  clickInside() {
    this.wasInside = true;
  }

  @HostListener('document:keydown.escape', ['$event'])
  onKeydownHandler(event: KeyboardEvent) {
    this.close();
    this.wasInside = false;
  }

  @HostListener('window:click', ['$event'])
  keyEvent(event: PointerEvent) {
    if (event.composedPath().length === this.composedPathLength) {
      this.close();
      this.wasInside = false;
    }
  }

  ngOnInit() {
    this.createToolbarOptions();
  }

  ngAfterViewInit(): void {
    this.loadChildComponent(this.childComponentType!);

    if (this.options?.title) {
      const headerElement = document
        .getElementsByClassName('p-sidebar-header')
        .item(0);
      const closeButtonElement = document
        .getElementsByClassName('p-sidebar-close')
        .item(0);

      if (headerElement && closeButtonElement) {
        const titleElement = this.renderer2.createElement('h4');

        this.renderer2.addClass(headerElement, 'justify-content-between');
        this.renderer2.setProperty(
          titleElement,
          'innerHTML',
          T(this.options.title)
        );
        this.renderer2.insertBefore(
          headerElement,
          titleElement,
          closeButtonElement
        );
      }
    }
    this.cd.detectChanges();
  }

  ngOnDestroy(): void {
    if (this.componentRef) {
      this.componentRef.destroy();
    }

    this.onDestroy.next(null);
    this.onDestroy.complete();
  }

  loadChildComponent(componentType: Type<any>): void {
    let componentFactory =
      this.componentFactoryResolver.resolveComponentFactory(componentType);

    let viewContainerRef = this.insertionPoint.viewContainerRef;
    viewContainerRef.clear();

    this.componentRef = viewContainerRef.createComponent(componentFactory);

    this.initializeSubjects(this.componentRef.instance);
    this.addToolbar();
  }

  initializeSubjects(component: ICrudComponent) {
    component.onResult
      .pipe(takeUntil(this.onDestroy))
      .subscribe((result: SidebarDialogResult) => {
        this.options?.onResult(result);
      });

    if (component.onInitializing) {
      component.onInitializing
        .pipe(takeUntil(this.onDestroy))
        .subscribe((result: any) => {
          if (result) {
            this.saveButton?.onLoading!.next(result.saving);
            this.saveButton?.onDisabling!.next(result.loading);

            this.updateButton?.onLoading!.next(result.updating);
            this.updateButton?.onDisabling!.next(result.loading);

            this.cancelButton?.onDisabling!.next(
              result.saving || result.updating
            );
          }
        });
    }

    if (this.options?.data) component.setData(this.options?.data);
  }

  addToolbar() {
    const sidebarElement = document.getElementsByClassName('p-sidebar').item(0);
    if (this.hasFooter)
      this.renderer2.appendChild(sidebarElement, this.footer.nativeElement);
    this.initContentElement();
  }

  initContentElement() {
    const contentElement = document
      .getElementsByClassName('p-sidebar-content')
      .item(0);
    this.renderer2.setStyle(contentElement, 'padding-bottom', '72px');
  }

  close(status?: SidebarDialogResultStatus): void {
    this._onClose.next(null);
    this.options?.onResult({
      status: status ?? SidebarDialogResultStatus.cancel,
    });
  }

  back(): void {
    this.close(SidebarDialogResultStatus.back);
  }

  get size(): SidebarDialogSize {
    return this.options?.size || SidebarDialogSize.medium;
  }

  get position(): Position {
    return this.options?.position || 'right';
  }

  get dismissible(): boolean {
    return this.options?.dismissible || false;
  }

  get fullScreen(): boolean {
    return this.options?.fullScreen || false;
  }

  createToolbarOptions() {
    const tOptions: IToolbarOptions = {
      defaultButtons: {
        position: 'right',
      },
    };

    if (this.options?.buttons?.save) {
      tOptions!.defaultButtons!.save = {
        onClick: () => {
          if (this.componentRef?.instance.save) {
            this.componentRef?.instance.save();
          }
        },
        onLoading: new Subject<boolean>(),
        onDisabling: new Subject<boolean>(),
      };
    }

    if (this.options?.buttons?.update) {
      tOptions!.defaultButtons!.update = {
        onClick: () => {
          if (this.componentRef?.instance.update) {
            this.componentRef?.instance.update();
          }
        },
        onLoading: new Subject<boolean>(),
        onDisabling: new Subject<boolean>(),
      };
    }

    if (this.options?.buttons?.cancel) {
      tOptions!.defaultButtons!.cancel = {
        onClick: () => {
          if (this.componentRef?.instance.cancel)
            this.componentRef?.instance.cancel();
          else this.close();
        },
        onDisabling: new Subject<boolean>(),
      };
    }

    this.toolbarOptions = tOptions;
  }

  get saveButton() {
    return this.toolbarOptions?.defaultButtons?.save;
  }

  get updateButton() {
    return this.toolbarOptions?.defaultButtons?.update;
  }

  get cancelButton() {
    return this.toolbarOptions?.defaultButtons?.cancel;
  }

  // get nextButton() {
  //   return this.toolbarOptions?.defaultButtons?.next;
  // }

  // get previousButton() {
  //   return this.toolbarOptions?.defaultButtons?.previous;
  // }

  get hasFooter() {
    return (
      this.options?.buttons?.cancel ||
      this.options?.buttons?.save ||
      this.options?.buttons?.update ||
      this.options?.buttons?.next ||
      this.options?.buttons?.previous
    );
  }
}
