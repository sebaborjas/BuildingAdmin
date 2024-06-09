import { Component } from '@angular/core';
import { LoadingService } from '../../../services/loading.service';
import { NgIf, NgFor, CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TicketService } from '../../../services/ticket.service';
import { HotToastService } from '@ngneat/hot-toast';
import { StatusTypes } from '../statusTypes';
import { TicketModel, Category, AssignedTo } from '../../../services/types';

@Component({
  selector: 'app-maintenance-tickets',
  standalone: true,
  imports: [NgIf, FormsModule, NgFor, CommonModule],
  templateUrl: './tickets.component.html',
  styleUrl: './tickets.component.css'
})
export class TicketsMaintenanceComponent {
  constructor(
    private _loadingService: LoadingService,
    private _ticketService: TicketService,
    private _toastService: HotToastService
  ) {
    this._loadingService.loadingOn();
  }

  isVisibleCompleteTicket = false;
  isVisibleTicketDetails = false;
  assignedTickets: any = [];
  totalCost: number = 0;
  selectedCompleteTicketId: number = 0;
  selectedTicketDetails: TicketModel = {
    id: 0,
    description: '',
    creationDate: '',
    apartment: null,
    totalCost: 0,
    createdBy: null,
    category: {
      name: '',
      id: 0,
      relatedTo: null
    },
    status: '',
    attentionDate: '',
    closingDate: '',
    assignedTo: {
      lastName: '',
      buildings: null,
      id: 0,
      name: '',
      email: '',
      password: ''
    }
  };

  showCompleteTicketModal(id: number) {
    this.isVisibleCompleteTicket = true;
    this.selectedCompleteTicketId = id;
  }

  hideCompleteTicketModal() {
    this.isVisibleCompleteTicket = false;
    this.selectedCompleteTicketId = 0;
    this.totalCost = 0;
  }

  showTicketDetails(ticket: TicketModel) {
    this.isVisibleTicketDetails = true;
    this.selectedTicketDetails = JSON.parse(JSON.stringify(ticket));
    this.selectedTicketDetails.creationDate = new Date(this.selectedTicketDetails.creationDate).toLocaleString();
    this.selectedTicketDetails.attentionDate = new Date(this.selectedTicketDetails.attentionDate).toLocaleString();
    this.selectedTicketDetails.closingDate = new Date(this.selectedTicketDetails.closingDate).toLocaleString();
  }

  hideTicketDetails() {
    this.isVisibleTicketDetails = false;
  }

  ngOnInit() {
    this.getAssignedTickets();
  }

  getAssignedTickets() {
    this._loadingService.loadingOn();
    this._ticketService.GetAssignedTickets()
    .pipe(
      this._toastService.observe({
        loading: 'Cargando tickets asignados',
        success: 'Tickets asignados cargados con exito',
        error: 'Error al cargar los tickets asignados',
      })
    )
    .subscribe(
      (response) => {
        this.assignedTickets = response;
        this.assignedTickets.forEach((ticket: TicketModel) => {
          ticket.status = StatusTypes[parseInt(ticket.status)];
        });
        this._loadingService.loadingOff();
      },
      (error) => {
        this._loadingService.loadingOff();
      }
    );
    this._loadingService.loadingOff();
  }

  attendTicket(id: number) {
    this._loadingService.loadingOn();
    this._ticketService.StartTicket(id)
    .pipe(
      this._toastService.observe({
        loading: 'Atendiendo ticket',
        success: 'Ticket atendido con exito',
        error: 'Error al atender ticket',
      })
    )
    .subscribe(
      (response) => {
        this.getAssignedTickets();
        this._loadingService.loadingOff();
      },
      (error) => {
        this._loadingService.loadingOff();
      }
    );
  }

  finishTicket() {
    if (this.totalCost === 0 || this.selectedCompleteTicketId === 0) {
      this._toastService.error('Debe ingresar un costo total');
      return;
    }
    this._loadingService.loadingOn();
    this._ticketService.CompleteTicket(this.selectedCompleteTicketId, this.totalCost)
    .pipe(
      this._toastService.observe({
        loading: 'Finalizando ticket',
        success: 'Ticket finalizado con exito',
        error: 'Error al finalizar ticket',
      })
    )
    .subscribe(
      (response) => {
        this._loadingService.loadingOff();
        this.getAssignedTickets();
      },
      (error) => {
        this._loadingService.loadingOff();
      }
    );
    this.hideCompleteTicketModal();
  }
}
