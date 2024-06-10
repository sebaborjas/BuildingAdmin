import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginEndpoint } from '../../networking/endpoints';
import { Observable } from 'rxjs';
import { LoginModel } from './types';
import { environment } from '../../environments/environment.development';


const BASE_URL = environment.API_URL;


@Injectable({
  providedIn: 'root'
})
export class AuthService {



  constructor(private _httpClient: HttpClient) { }

  login(email: string, password: string): Observable<LoginModel> {
    const body = {
      email: email,
      password: password
    }
    return this._httpClient.post<LoginModel>(`${BASE_URL}${LoginEndpoint.LOGIN}`, body);
  }
}
