import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { InvitationEndpoint } from '../../networking/endpoints';
import { Observable } from 'rxjs';
import { AcceptInvitationOutputModel, RejectInvitationModel, AcceptInvitationModel } from './types';
import { environment } from '../../environments/environment.development';

const BASE_URL = environment.API_URL;

@Injectable({
  providedIn: 'root'
})
export class InvitationService {

  constructor(private _httpClient: HttpClient) { }

  acceptInvitation(acceptInput: AcceptInvitationModel): Observable<AcceptInvitationOutputModel> {
    return this._httpClient.put<AcceptInvitationOutputModel>(`${BASE_URL}${InvitationEndpoint.ACCEPT}`, acceptInput);
  }

  rejectInvitation(rejectInput: RejectInvitationModel): Observable<void> {
    return this._httpClient.put<void>(`${BASE_URL}${InvitationEndpoint.REJECT}`, rejectInput);
  }

}
