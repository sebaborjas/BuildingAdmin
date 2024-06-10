import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { LoadingService } from '../../../services/loading.service';
import { BuildingsService } from '../../../services/buildings.service';
import { CategoryService } from '../../../services/category.service';
import { TicketService } from '../../../services/ticket.service';
import { HotToastService } from '@ngneat/hot-toast';
import { StatusTypes } from '../statusTypes';
import { BuildingModel, CategoryModel, TicketCreateModel, ApartmentModel, TicketModel } from '../../../services/types';

@Component({
  selector: 'app-manager-tickets',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './tickets.component.html',
  styleUrl: './tickets.component.css'
})
export class TicketsManagerComponent implements OnInit {

  constructor(
    private _loadingService: LoadingService,
    private _toastService: HotToastService,
    private _buildingService: BuildingsService,
    private _categoryService: CategoryService,
    private _ticketService: TicketService
  ) { }

  isVisibleModal: boolean = false;
  isDetailModelVisible = false;
  selectedTicket: TicketModel = {
    id: 0,
    description: '',
    creationDate: '',
    apartment: {
      id: 0,
      floor: 0,
      doorNumber: 0,
      ownerName: '',
      ownerLastName: '',
      ownerEmail: '',
      rooms: 0,
      bathrooms: 0,
      hasTerrace: false
    },
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
  description: string = '';
  categorySelected: number = 0;
  apartmentSelected: number = 0;
  buildingSelected: BuildingModel = {
    id: 0,
    name: '',
    address: '',
    location: '',
    managerName: '',
    expenses: 0,
    apartments: []
  };

  buildings: BuildingModel[] = [];
  categories: CategoryModel[] = [];
  apartments: ApartmentModel[] = [];
  tickets: TicketModel[] = [];

  ngOnInit(): void {
    this.getBuildings();
    this.getCategories();
  
   this.getTickets();
  }

  showMoreInfo(ticket: TicketModel) {
    this.selectedTicket = ticket;
    this.isDetailModelVisible = true;
  }

  hideMoreInfo() {
    this.isDetailModelVisible = false;
  }

  showModal() {
    this.isVisibleModal = true;
  }

  hideModal() {
    this.isVisibleModal = false;
  }

  onBuildingChange(event: Event) {
    const buildingId = (event.target as HTMLSelectElement).value;

    const selectedBuilding = this.buildings.find(building => building.id == +buildingId);

    if (selectedBuilding) {
      this.apartments = selectedBuilding.apartments;
    } else {
      this.apartments = [];
    }
  }

  createTicket() {
    this._loadingService.loadingOn();
    const ticketCreate: TicketCreateModel = {
      description: this.description,
      categoryId: this.categorySelected,
      apartmentId: this.apartmentSelected
    };
    console.log(ticketCreate);
    this._ticketService.createTicket(ticketCreate)
      .pipe(
        this._toastService.observe({
          loading: 'Creando ticket',
          success: 'Ticket creado con éxito',
          error: (e) => e?.error || 'Error al crear ticket'
        })
      )
      .subscribe((response: TicketModel) => {
        this.hideModal();
        this._loadingService.loadingOff();
      },
        (error) => {
          console.log(error);
          this._loadingService.loadingOff();
        }
      );
  }

  getBuildings() {
    this._loadingService.loadingOn();
    this._buildingService.getBuildings()
      .pipe(
        this._toastService.observe({
          loading: 'Obteniendo edificios',
          success: 'Edificios obtenidos con éxito',
          error: (e) => e?.error || 'Error al obtener edificios'
        })
      )
      .subscribe((response: BuildingModel[]) => {
        this.buildings = response;
        this._loadingService.loadingOff();
      },
        (error) => {
          console.log(error);
          this._loadingService.loadingOff();
        });
  }

  getCategories() {
    this._loadingService.loadingOn();
    this._categoryService.getCategories()
      .pipe(
        this._toastService.observe({
          loading: 'Obteniendo categorías',
          success: 'Categorías obtenidas con éxito',
          error: (e) => e?.error || 'Error al obtener categorías'
        })
      )
      .subscribe((response: CategoryModel[]) => {
        this.categories = response;
        this._loadingService.loadingOff();
      },
        (error) => {
          console.log(error);
          this._loadingService.loadingOff();
        }
      );
  }

  getTickets() {
    this._loadingService.loadingOn();
    this._ticketService.GetTickets()
      .pipe(
        this._toastService.observe({
          loading: 'Obteniendo tickets',
          success: 'Tickets obtenidos con éxito',
          error: 'Error al obtener tickets'
        })
      )
      .subscribe((response: TicketModel[]) => {
        console.log(response);
        this.tickets = response;
        this.tickets.forEach((ticket: TicketModel) => {
          ticket.status = StatusTypes[parseInt(ticket.status)];
        });
       
        this._loadingService.loadingOff();
      },
        (error) => {
          console.log(error);
          this._loadingService.loadingOff();
        }
      );
    }
}
