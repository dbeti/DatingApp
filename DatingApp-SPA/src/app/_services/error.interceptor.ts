import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HTTP_INTERCEPTORS} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    private APPLICATION_ERROR_HEADER = 'Application-Error';

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(
            catchError(errorResponse => {
                if (errorResponse instanceof HttpErrorResponse) {
                    const serverError = errorResponse.error;

                    if (serverError.status === 401) {
                        return throwError(serverError.title);
                    }

                    const error = errorResponse.headers.get(this.APPLICATION_ERROR_HEADER);

                    if (error) {
                        return throwError(error);
                    }

                    let modalStateErrors = '';

                    if (serverError && serverError.errors && typeof serverError.errors === 'object') {
                        for (const key of Object.keys(serverError.errors)) {
                            if (serverError.errors[key]) {
                                modalStateErrors += serverError.errors[key] + '\n';
                            }
                        }
                    }

                    return throwError(modalStateErrors || serverError || 'Server Error');
                }
            })
        );
    }
}

export const ErrorInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi: true
};
