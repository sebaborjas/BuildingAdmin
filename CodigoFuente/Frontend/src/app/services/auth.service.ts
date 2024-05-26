import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginEndpoint } from '../../networking/endpoints';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private _httpClient: HttpClient) { }

  login(email: string, password: string) {
    const body = {
      email: email,
      password: password
    }
    return this._httpClient.post(LoginEndpoint.LOGIN, body);
  }
}
