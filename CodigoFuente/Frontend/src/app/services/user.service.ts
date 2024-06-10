import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { UserEndpoint } from '../../networking/endpoints';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { CompanyAdministratorModel, CreateCompanyAdministratorModel, ManagerModel, UserModel, MaintenanceOperatorModel, MaintenanceOperatorModelOut } from './types';
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

    return this._httpClient.get<UserModel>(`${BASE_URL}${UserEndpoint.USER}`, {
      params: params,
    });
  }

  getManagers(): Observable<ManagerModel[]> {
    return this._httpClient.get<ManagerModel[]>(`${BASE_URL}${UserEndpoint.MANAGER}`);
  }

  getCompanyAdministrators(): Observable<CompanyAdministratorModel[]>{
    return this._httpClient.get<CompanyAdministratorModel[]>(`${BASE_URL}${UserEndpoint.COMPANYADMINISTRATOR}`);
  }

  createCompanyAdministrator(administrator: CreateCompanyAdministratorModel): Observable<CompanyAdministratorModel>{
    return this._httpClient.post<CompanyAdministratorModel>(`${BASE_URL}${UserEndpoint.COMPANYADMINISTRATOR}`, administrator);
  }

  createMaintenanceOperator(maintenanceOperator: MaintenanceOperatorModel): Observable<MaintenanceOperatorModel>{
    return this._httpClient.post<MaintenanceOperatorModel>(`${BASE_URL}${UserEndpoint.MAINTENANCEOPERATOR}`, maintenanceOperator);
  }

  getMaintenanceOperators(): Observable<MaintenanceOperatorModelOut[]>{
    return this._httpClient.get<MaintenanceOperatorModelOut[]>(`${BASE_URL}${UserEndpoint.MAINTENANCEOPERATOR}`);
  }

}
