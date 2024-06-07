import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ConstructionCompany } from '../../networking/endpoints';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { ConstructionCompanyModel } from './types';

@Injectable({
  providedIn: 'root'
})
export class ConstructionCompanyService {

  constructor(private _httpClient: HttpClient) { }

  getConstructionCompany(): Observable<ConstructionCompanyModel[]> {
    return this._httpClient.get<ConstructionCompanyModel[]>(`${environment.API_URL}${ConstructionCompany.CONSTRUCTION_COMPANIES}`);
  }

  saveConstructionCompany(name: string): Observable<ConstructionCompanyModel> {
    return this._httpClient.post<ConstructionCompanyModel>(`${environment.API_URL}${ConstructionCompany.CONSTRUCTION_COMPANIES}`, { name: name });
  }

}
