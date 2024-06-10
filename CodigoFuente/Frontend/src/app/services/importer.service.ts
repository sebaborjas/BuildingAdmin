import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { ImporterEndpoint } from '../../networking/endpoints';
import { Observable } from 'rxjs';
import { ImporterInputModel, ImporterOutputModel } from './types';

@Injectable({
  providedIn: 'root'
})
export class ImporterService {

  constructor(private _httpClient: HttpClient) { }

  getImporters(): Observable<string[]>{
    return this._httpClient.get<string[]>(`${environment.API_URL}${ImporterEndpoint.IMPORTERS}`);
  }

  importBuildings(importerInput: ImporterInputModel): Observable<ImporterOutputModel>{
    return this._httpClient.post<ImporterOutputModel>(`${environment.API_URL}${ImporterEndpoint.IMPORTERS}`, importerInput);
  }
}
