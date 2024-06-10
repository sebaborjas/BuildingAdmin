import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ReportEndpoint } from '../../networking/endpoints';
import { Observable } from 'rxjs';
import { TicketsByApartmentsModel, TicketsByBuildingModel, TicketsByCategoriesModel, TicketsByMaintenanceOperatorModel } from './types';
import { environment } from '../../environments/environment.development';

const BASE_URL = environment.API_URL;

@Injectable({
  providedIn: 'root',
})
export class ReportService {
  constructor(private _httpClient: HttpClient) { }

  getReportTicketsByCategories(name: string, category?: string): Observable<TicketsByCategoriesModel[]> {
    let params = new HttpParams().set('building', name);

    if (category) {
      params = params.set('category', category);
    }

    return this._httpClient.get<TicketsByCategoriesModel[]>(
      `${BASE_URL}${ReportEndpoint.REPORTS}/categories`,
      { params: params }
    );
  }

  getReportBuildings(building?: string): Observable<TicketsByBuildingModel[]> {

    if (building) {
      let params = new HttpParams().set('building', building);

      return this._httpClient.get<TicketsByBuildingModel[]>(
        `${BASE_URL}${ReportEndpoint.REPORTS}/buildings`,
        { params: params }
      );
    }

    return this._httpClient.get<TicketsByBuildingModel[]>(
      `${BASE_URL}${ReportEndpoint.REPORTS}/buildings`
    );
  }

  getReportTicketsByApartment(building?: string): Observable<TicketsByApartmentsModel[]> {

    if (building) {
      let params = new HttpParams().set('building', building);

      return this._httpClient.get<TicketsByApartmentsModel[]>(
        `${BASE_URL}${ReportEndpoint.REPORTS}/tickets-by-apartment`,
        { params: params }
      );
    }

    return this._httpClient.get<TicketsByApartmentsModel[]>(
      `${BASE_URL}${ReportEndpoint.REPORTS}/tickets-by-apartment`
    );
  }

  getReportOperators(building: string, name?: string): Observable<TicketsByMaintenanceOperatorModel[]> {

    let params = new HttpParams().set('building', building);

    if (name) {
      params = params.set('name', name);
    }

    return this._httpClient.get<TicketsByMaintenanceOperatorModel[]>(
      `${BASE_URL}${ReportEndpoint.REPORTS}/operators`,
      { params: params }
    );
  }
}
