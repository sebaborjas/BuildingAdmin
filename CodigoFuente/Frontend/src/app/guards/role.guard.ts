import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { UserService } from '../services/user.service';
import { HotToastService } from '@ngneat/hot-toast';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable()
export class RoleGuard implements CanActivate {

  constructor(
    private _router: Router,
    private _toastService: HotToastService,
    private _userService: UserService
  ) { }

  public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
    const expectedRole = route.data['expectedRole'];

    return this._userService.getUser().pipe(
      map((user) => {
        if (user.role === expectedRole) {
          return true;
        } else {
          this._toastService.error('No tienes permiso para acceder a esta página');
          this._router.navigate(['/home/dashboard']);
          return false;
        }
      })
    );
  }
}
