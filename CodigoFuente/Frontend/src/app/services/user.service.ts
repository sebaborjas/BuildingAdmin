import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { UserEndpoint } from '../../networking/endpoints';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { UserModel } from './types';
import { SesionStorageService } from './sesion-storage.service';

const BASE_URL = environment.API_URL;

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(
    private _httpClient: HttpClient,
    private _sesionStorageService: SesionStorageService
  ) {}

  getUser(): Observable<UserModel> {
    const token = this._sesionStorageService.getToken() || '';
    let params = new HttpParams().set('token', token);

    return this._httpClient.get<UserModel>(
      `${environment.API_URL}${UserEndpoint.USER}`,
      { params: params }
    );
  }
}
