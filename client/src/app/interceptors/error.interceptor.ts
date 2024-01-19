import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from '@angular/common/http';
import { catchError, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private router: Router,
    private toastr: ToastrService,
  ) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler,
  ): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error) {
          switch (error.status) {
            case 401:
              this.toastr.error(
                `${error.error.errorMessage}`,
                `${error.status.toString()} Unauthorised`,
              );
              break;
            case 400:
              if (error.error.errorMessage) {
                this.toastr.error(
                  `${error.error.errorMessage}`,
                  `${error.status.toString()} Bad Request`,
                );
              } else if (error.error.errors) {
                for (let e in error.error.errors) {
                  this.toastr.error(
                    `${error.error.errors[e][0]}`,
                    `${error.status.toString()} Bad Request`,
                  );
                }
              }
              console.log(error);
              break;
            case 403:
              this.toastr.error(
                `${error.error.errorMessage}`,
                `${error.status.toString()} Forbidden`,
              );
              break;
            case 404:
              this.router.navigateByUrl('/not-found');
              break;
            default:
              this.toastr.error('Something unexpected went wrong');
              this.toastr.error(error.error.errorMessage);
              console.log(error);
              break;
          }
        }
        throw error;
      }),
    );
  }
}
