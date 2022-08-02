import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse
} from '@angular/common/http';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';

import { IResult } from '../models';
import { environment } from 'src/environments/environment';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(
    private router: Router, private messageService: MessageService
  ) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      tap(evt => {
        if (evt instanceof HttpResponse) {
          const result = evt.body as IResult<any>;
          // auto logout if 401 response returned from api
          if (result.statusCode?.toString() == '401') {
            if(result.message)
              setTimeout(() => { this.messageService.add({ severity: "error", detail: result.message }); }, 1111)
            //this.authService.logout();
          }
          if (result && result.isOk && result.message) {
            setTimeout(() => { this.messageService.add({ severity: "success", detail: result.message }); }, 1111)
          } else if (result && !result.isOk && result.message) {
            this.messageService.add({ severity: "error", detail: result.message });
          }
        }
      }),
      catchError(err => {
        if (err.status === 401 || err.error?.statusCode === 401) {
          // auto logout if 401 response returned from api
          if (err.error.message) {
            this.messageService.add({ severity: "error", detail: err.error.message });
          }
          setTimeout(() => {
            //this.authService.logout();
            //this.router.navigate([environment.paths.login]);
          }, 2000);
          return throwError(err.error.message);
        } else {
          if (err.error?.message)
            this.messageService.add({ severity: "error", detail: err.error.message });
          else
            this.messageService.add({ severity: "error", detail: "Bir hata oluştu..." });
          const error = err.error?.message || err.statusText;
          return throwError(error);
        }
      }))
  }
}
