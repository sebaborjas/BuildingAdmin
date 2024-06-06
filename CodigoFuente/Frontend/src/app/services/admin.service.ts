import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { InvitationEndpoint } from '../../networking/endpoints';
import { Observable } from 'rxjs';
import { InvitationModel, EditInvitationModel } from './types';
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
}
