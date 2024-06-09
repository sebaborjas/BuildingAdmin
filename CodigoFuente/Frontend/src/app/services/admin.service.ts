import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { InvitationEndpoint, ReportEndpoint, CategoriesEndpoint, UserEndpoint, BuildingEndpoint } from '../../networking/endpoints';
import { Observable } from 'rxjs';
import {
  InvitationModel,
  EditInvitationModel,
  CategoryModel,
  AdminModel,
  CreateCategoryModel,
  BuildingModel,
  TicketModel,
  TicketsByCategoriesModel,
} from './types';
import { environment } from '../../environments/environment.development';

const BASE_URL = environment.API_URL;

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  constructor(private _httpClient: HttpClient) { }

  createInvitation(
    name: string,
    email: string,
    role: number,
    expirationDate: string
  ): Observable<InvitationModel> {
    const body = {
      email: email,
      name: name,
      role: role,
      expirationDate: expirationDate,
    };
    return this._httpClient.post<InvitationModel>(
      `${BASE_URL}${InvitationEndpoint.INVITATIONS}`,
      body
    );
  }

  editInvitation(
    id: number,
    expirationDate: string
  ): Observable<EditInvitationModel> {
    const body = {
      id: id,
      expirationDate: expirationDate

    };
    return this._httpClient.put<EditInvitationModel>(
      `${BASE_URL}${InvitationEndpoint.INVITATIONS}/${id}`,
      body
    );
  }

  getAllInvitations(): Observable<InvitationModel[]> {
    return this._httpClient.get<InvitationModel[]>(
      `${BASE_URL}${InvitationEndpoint.INVITATIONS}`
    );
  }

  deleteInvitation(id: number): Observable<InvitationModel> {
    return this._httpClient.delete<InvitationModel>(
      `${BASE_URL}${InvitationEndpoint.INVITATIONS}/${id}`
    );
  }

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

  getCategories(id?: string): Observable<CategoryModel[]> {
    let params = new HttpParams()

    if (id) {
      params = params.set('id', id);
    }

    return this._httpClient.get<CategoryModel[]>(
      `${BASE_URL}${CategoriesEndpoint.CATEGORIES}`,
      { params: params }
    );
  }

  getBuildings(): Observable<BuildingModel[]> {
    return this._httpClient.get<BuildingModel[]>(
      `${BASE_URL}${BuildingEndpoint.BUILDINGS}`,
    );
  }

  createCategory(name: string, parentCategoryId?: number): Observable<CreateCategoryModel> {
    const body = {
      name: name,
      parentCategoryId: parentCategoryId
    };
    return this._httpClient.post<CreateCategoryModel>(
      `${BASE_URL}${CategoriesEndpoint.CATEGORIES}`,
      body
    );
  }

  createAdministartor(name: string, lastName: string, email: string, password: string): Observable<AdminModel> {
    const body = {
      name: name,
      lastName: lastName,
      email: email,
      password: password
    };
    return this._httpClient.post<AdminModel>(
      `${BASE_URL}${UserEndpoint.ADMINISTRATOR}`,
      body
    );
  }
}
