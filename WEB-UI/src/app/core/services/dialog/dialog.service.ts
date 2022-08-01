import { ApplicationRef, ComponentFactoryResolver, ComponentRef, EmbeddedViewRef, Injectable, Injector, Type } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Route, Router } from '@angular/router';
import { DialogRef, ISidebarDialogOptions } from '../../components/dialogs/sidebar-dialog/models';
import { SidebarDialogComponent } from '../../components/dialogs/sidebar-dialog/sidebar-dialog.component';
import { DialogInjector } from '../../injectors';

@Injectable({
  providedIn: 'root'
})
export class DialogService {
  sidebarDialogComponentRef!: ComponentRef<SidebarDialogComponent>;
  dialogRef?: DialogRef;
  constructor(
    private dom: DomSanitizer,
    private componentFactoryResolver: ComponentFactoryResolver,
    private appRef: ApplicationRef,
    private injector: Injector,
    private router:Router
  ) {
    router.events.subscribe((event:any)=> this.close());
   }

   public open(componentType: Type<any>, options?: ISidebarDialogOptions): DialogRef {
    this.close();
    this.dialogRef = this.appendDialogComponentToBody(options);

    this.sidebarDialogComponentRef.instance.childComponentType = componentType;
    this.sidebarDialogComponentRef.instance.display = true;
    return this.dialogRef;
  }

  private appendDialogComponentToBody(options?: any): DialogRef {
    const map = new WeakMap();

    const dialogRef = new DialogRef();
    map.set(DialogRef, dialogRef);

    const sub = dialogRef.afterClosed.subscribe(() => {
      // close the dialog
      this.removeDialogComponentFromBody();
      sub.unsubscribe();
    });

    const componentFactory = this.componentFactoryResolver.resolveComponentFactory(SidebarDialogComponent);
    const componentRef = componentFactory.create(new DialogInjector(this.injector, map));
    this.appRef.attachView(componentRef.hostView);

    const domElem = (componentRef.hostView as EmbeddedViewRef<any>).rootNodes[0] as HTMLElement;
    document.body.appendChild(domElem);

    this.sidebarDialogComponentRef = componentRef;
    this.sidebarDialogComponentRef.instance.options = options;
    
    this.sidebarDialogComponentRef.instance.onClose.subscribe(() => {
      this.removeDialogComponentFromBody();
    });

    return dialogRef;
  }

  private removeDialogComponentFromBody(): void {
    this.appRef.detachView(this.sidebarDialogComponentRef.hostView);
    this.sidebarDialogComponentRef.destroy();
  }

  public close(result?: any) {
    if (this.dialogRef)
      this.dialogRef.close(result);
  }
}
