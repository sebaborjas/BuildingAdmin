import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TicketEndpoint } from '../../networking/endpoints';
import { Observable } from 'rxjs';
import { TicketModel, TicketCreateModel } from './types';
import { environment } from '../../environments/environment.development';

const BASE_URL = environment.API_URL;

@Injectable({
  providedIn: 'root',
})
export class TicketService {
  constructor(private _httpClient: HttpClient) { }

  GetAssignedTickets(): Observable<TicketModel[]> {
    return this._httpClient.get<TicketModel[]>(
      `${BASE_URL}${TicketEndpoint.TICKETS}/assigned`
    );
  }

  StartTicket(id: number): Observable<TicketModel> {
    return this._httpClient.put<TicketModel>(
      `${BASE_URL}${TicketEndpoint.TICKETS}/${id}/start`,
      {}
    );
  }

  CompleteTicket(id: number, totalCost: number): Observable<TicketModel> {
    return this._httpClient.put<TicketModel>(
      `${BASE_URL}${TicketEndpoint.TICKETS}/${id}/complete`,
      { totalCost }
    );
  }

  createTicket(ticket: TicketCreateModel): Observable<TicketModel> {
    return this._httpClient.post<TicketModel>(
      `${BASE_URL}${TicketEndpoint.TICKETS}`,
      { ticket }
    );
  }
  
}