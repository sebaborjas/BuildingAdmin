import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ReportEndpoint } from '../../networking/endpoints';
import { Observable } from 'rxjs';
import { TicketsByCategoriesModel } from './types';
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
      `${BASE_URL}${ReportEndpoint.REPORT_TICKETS_BY_CATEGORIES}`,
      { params: params }
    );
  }

}
