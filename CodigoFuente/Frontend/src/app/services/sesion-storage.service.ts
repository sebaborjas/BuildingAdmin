import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SesionStorageService {
  private _userToken = 'userToken';

  constructor() {}

  setToken(token: string) {
    sessionStorage.setItem(this._userToken, token);
  }

  getToken(): string | null {
    return sessionStorage.getItem(this._userToken);
  }

  removeToken() {
    sessionStorage.removeItem(this._userToken);
  }
}
