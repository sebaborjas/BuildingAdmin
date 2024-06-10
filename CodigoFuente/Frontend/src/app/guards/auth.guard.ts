import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { SesionStorageService } from '../services/sesion-storage.service';
import { HotToastService } from '@ngneat/hot-toast';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private _router: Router, private _sesionStorageService: SesionStorageService, private _toastService: HotToastService) { }

  public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (!this._sesionStorageService.getToken()) {
      this._router.navigate(['/login']);
      this._toastService.error('Debe iniciar sesion para acceder a esta pagina.');
      return false;
    }

    return true;
  }
}