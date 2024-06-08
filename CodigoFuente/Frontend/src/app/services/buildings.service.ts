import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment.development';
import { BuildingEndpoint } from '../../networking/endpoints';
import { BuildingModel, CreateBuildingModel } from './types';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BuildingsService {

  constructor(private _httpClient: HttpClient) { }

  getBuildings(): Observable<BuildingModel[]>{
    return this._httpClient.get<BuildingModel[]>(`${environment.API_URL}${BuildingEndpoint.BUILDINGS}`);
  }

  createBuilding(building: CreateBuildingModel){
    return this._httpClient.post(`${environment.API_URL}${BuildingEndpoint.BUILDINGS}`, building);
  }
}
