import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { InvitationEndpoint, UserEndpoint} from '../../networking/endpoints';
import { Observable } from 'rxjs';
import {
  InvitationModel,
  EditInvitationModel,
  AdminModel,
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
