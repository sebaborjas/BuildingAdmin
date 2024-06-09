import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { HotToastService } from '@ngneat/hot-toast';

@Injectable()
export class ConnectionInterceptor implements HttpInterceptor {
  constructor(private router: Router, private toastService: HotToastService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        if (!navigator.onLine) {
          this.toastService.error('No hay conexión a internet.');
          this.router.navigate(['/login']);
        } else if (error.status === 0) {
          this.toastService.error('No se pudo conectar con el servidor.');
          this.router.navigate(['/login']);
        }
        return throwError(error);
      })
    );
  }
}
