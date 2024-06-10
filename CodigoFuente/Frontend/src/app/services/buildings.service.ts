import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment.development';
import { BuildingEndpoint } from '../../networking/endpoints';
import { BuildingModel, CreateBuildingModel, CreateBuildingOutputModel, ModifyBuildingModel } from './types';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BuildingsService {

  constructor(private _httpClient: HttpClient) { }

  getBuildings(): Observable<BuildingModel[]>{
    return this._httpClient.get<BuildingModel[]>(`${environment.API_URL}${BuildingEndpoint.BUILDINGS}`);
  }

  createBuilding(building: CreateBuildingModel): Observable<CreateBuildingOutputModel> {
    return this._httpClient.post<CreateBuildingOutputModel>(`${environment.API_URL}${BuildingEndpoint.BUILDINGS}`, building);
  }

  deleteBuilding(buildingId: number): Observable<void>{
    return this._httpClient.delete<void>(`${environment.API_URL}${BuildingEndpoint.BUILDINGS}/${buildingId}`);
  }

  modifyManager(buildingId: number, managerId: number): Observable<void>{
    return this._httpClient.put<void>(`${environment.API_URL}${BuildingEndpoint.BUILDINGS}/${buildingId}/manager/`, { managerId: managerId });
  }

  modifyBuilding(buildingInput: ModifyBuildingModel, buildingId: number): Observable<void>{
    return this._httpClient.put<void>(`${environment.API_URL}${BuildingEndpoint.BUILDINGS}/${buildingId}`, buildingInput);
  }
}
