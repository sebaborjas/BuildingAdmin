import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { LoadingService } from '../../../services/loading.service';
import { BuildingsService } from '../../../services/buildings.service';
import { CategoryService } from '../../../services/category.service';
import { HotToastService } from '@ngneat/hot-toast';
import { BuildingModel, ApartmentModel, TicketsByBuildingModel, TicketsByApartmentsModel } from '../../../services/types';
import { ReportService } from '../../../services/report.service';

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
    private _categoryService: CategoryService,
    private _reportService: ReportService

  ) { }

  isVisibleReportBuilding: boolean = false;
  isVisibleReportApartment: boolean = false;
  apartmentSelected: number = 0;
  buildingSelected: string = '';

  buildings: BuildingModel[] = [];
  apartments: ApartmentModel[] = [];
  ticketsByBuilding: TicketsByBuildingModel[] = [];
  ticketsByApartment: TicketsByApartmentsModel[] = [];

  ngOnInit(): void {
    this.getBuilings();
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
          console.log(error);
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
        console.log(response);
        this._loadingService.loadingOff();
      },
        (error) => {
          console.log(error);
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
      .subscribe((response : TicketsByApartmentsModel[]) => {
        this.ticketsByApartment = response;
        console.log(response);
        this._loadingService.loadingOff();
      },
        (error) => {
          console.log(error);
          this._loadingService.loadingOff();
        }
      );
  }
  
}
