import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SesionStorageService } from '../services/sesion-storage.service';
import { Observable } from 'rxjs';

@Injectable()
export class AdminInterceptor implements HttpInterceptor {
  constructor(private _sesionStorqageService: SesionStorageService) { }

  intercept(
    req: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    const token = this._sesionStorqageService.getToken();
    let newRequest = req;
    if (req.url.includes('invitations') || req.url.includes('categories') || req.url.includes('administrator') || req.url.includes('buildings')) {
      return next.handle(req);
    }

    if (token) {
      const headers = req.headers.set('Authorization', token);
      newRequest = req.clone({ headers });
    }
    return next.handle(newRequest);
  }
}
