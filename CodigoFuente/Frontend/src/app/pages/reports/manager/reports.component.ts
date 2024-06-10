import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { LoadingService } from '../../../services/loading.service';
import { BuildingsService } from '../../../services/buildings.service';
import { HotToastService } from '@ngneat/hot-toast';
import { BuildingModel, ApartmentTicketModel, TicketsByBuildingModel, TicketsByApartmentsModel, TicketsByMaintenanceOperatorModel, MaintenanceOperatorModelOut } from '../../../services/types';
import { ReportService } from '../../../services/report.service';
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'app-manager-reports',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './reports.component.html',
  styleUrl: './reports.component.css'
})
export class ReportsManagerComponent {

  constructor(
    private _loadingService: LoadingService,
    private _toastService: HotToastService,
    private _buildingService: BuildingsService,
    private _reportService: ReportService,
    private _userService: UserService

  ) { }

  isVisibleReportBuilding: boolean = false;
  isVisibleReportApartment: boolean = false;
  isVisibleReportMaintenanceOperator: boolean = false;

  apartmentSelected: number = 0;
  buildingSelected: string = '';
  maintenanceOperatorSelected: string = '';

  buildings: BuildingModel[] = [];
  apartments: ApartmentTicketModel[] = [];
  maintenanceOperators: MaintenanceOperatorModelOut[] = [];

  ticketsByBuilding: TicketsByBuildingModel[] = [];
  ticketsByApartment: TicketsByApartmentsModel[] = [];
  ticketsByMaintenanceOperator: TicketsByMaintenanceOperatorModel[] = [];

  ngOnInit(): void {
    this.getBuilings();
    this.getMaintenanceOperators();
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

  getBuilings() {
    this._loadingService.loadingOn();
    this._buildingService.getBuildings()
      .subscribe((response: BuildingModel[]) => {
        this.buildings = response;
        this._loadingService.loadingOff();
      },
        (error) => {
          this._loadingService.loadingOff();
        }
      );
  }

  setBuildingName(event: Event) {
    const buildingName = (event.target as HTMLSelectElement).value;
    this.buildingSelected = buildingName;
  }

  setBuildingNameApartment(event: Event) {
    const buildingName = (event.target as HTMLSelectElement).value;
    this.buildingSelected = buildingName;
  }

  showReportByBuilding() {
    this.isVisibleReportBuilding = true;
    this.showReportBuilding();
  }

  clearReportByBuilding() {
    this.isVisibleReportBuilding = false;
    this.ticketsByBuilding = [];
  }

  showReportBuilding() {
    this._loadingService.loadingOn();
    this._reportService.getReportBuildings(this.buildingSelected)
      .subscribe((response) => {
        this.ticketsByBuilding = response;
        this._loadingService.loadingOff();
      },
        (error) => {
          this._loadingService.loadingOff();
        }
      );
  }

  showReportByApartment() {
    if (this.buildingSelected == '') {
      this._toastService.error('Debe seleccionar un edificio');
      return;
    }
    this.isVisibleReportApartment = true;
    this.getReportTicketsByApartment();
  }

  clearReportByApartment() {
    this.isVisibleReportApartment = false;
    this.ticketsByApartment = [];
  }

  getReportTicketsByApartment() {
    this._loadingService.loadingOn();
    this._reportService.getReportTicketsByApartment(this.buildingSelected)
      .subscribe((response: TicketsByApartmentsModel[]) => {
        this.ticketsByApartment = response;
        this._loadingService.loadingOff();
      },
        (error) => {
          this._loadingService.loadingOff();
        }
      );
  }

  setMaintenanceOperator(event: Event) {
    const maintenanceOperatorName = (event.target as HTMLSelectElement).value;
    this.maintenanceOperatorSelected = maintenanceOperatorName;
  }

  showReportByMaintenanceOperator() {
    if (this.buildingSelected == '') {
      this._toastService.error('Debe seleccionar un edificio');
      return;
    }
    this.isVisibleReportMaintenanceOperator = true;
    this.getReportOperators();
  }

  clearReportByMaintenanceOperator() {
    this.isVisibleReportMaintenanceOperator = false;
    this.ticketsByMaintenanceOperator = [];
  }

  getReportOperators() {
    this._loadingService.loadingOn();
    this._reportService.getReportOperators(this.buildingSelected, this.maintenanceOperatorSelected)
      .subscribe((response: TicketsByMaintenanceOperatorModel[]) => {
        this.ticketsByMaintenanceOperator = response;
        this._loadingService.loadingOff();
      },
        (error) => {
          this._loadingService.loadingOff();
        }
      );
  }
  getMaintenanceOperators() {
    this._loadingService.loadingOn();
    this._userService.getMaintenanceOperators()
      .pipe(
        this._toastService.observe({
          loading: 'Obteniendo operadores de mantenimiento',
          success: 'Operadores de mantenimiento obtenidos con éxito',
          error: 'Error al obtener operadores de mantenimiento'
        })
      )
      .subscribe((response: MaintenanceOperatorModelOut[]) => {
        this.maintenanceOperators = response;
        this._loadingService.loadingOff();
      },
        (error) => {
          this._loadingService.loadingOff();
        }
      );
  }



}
