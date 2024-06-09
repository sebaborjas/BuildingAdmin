import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { AuthService } from './services/auth.service';
import { SesionStorageService } from './services/sesion-storage.service';
import { LoadingService } from './services/loading.service';
import { AdminService } from './services/admin.service';
import { TicketService } from './services/ticket.service';
import {
  provideHttpClient,
  withInterceptorsFromDi,
} from '@angular/common/http';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './Interceptors/auth.interceptor';
import { UserInterceptor } from './Interceptors/user.interceptor';
import { AdminInterceptor } from './Interceptors/admin.interceptor';
import { MOperatorInterceptor } from './Interceptors/maintenance-operator.interceptor';
import { provideHotToastConfig } from '@ngneat/hot-toast';

import { AuthGuard } from './guards/auth.guard';
import { RoleGuard } from './guards/role.guard';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(withInterceptorsFromDi()),
    provideHotToastConfig(),
    AuthService,
    UserInterceptor,
    SesionStorageService,
    LoadingService,
    AdminService,
    TicketService,
    AuthGuard,
    RoleGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: UserInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AdminInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MOperatorInterceptor,
      multi: true,
    },
  ],
};
