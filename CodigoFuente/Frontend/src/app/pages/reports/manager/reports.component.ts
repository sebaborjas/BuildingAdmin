import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { LoadingService } from '../../../services/loading.service';
import { BuildingsService } from '../../../services/buildings.service';
import { HotToastService } from '@ngneat/hot-toast';
import { BuildingModel, ApartmentModel, TicketsByBuildingModel } from '../../../services/types';
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
    private _reportService: ReportService

  ) { }

  isVisibleReportBuilding: boolean = false;
  buildingSelected: string = '';

  buildings: BuildingModel[] = [];
  apartments: ApartmentModel[] = [];
  ticketsByBuilding: TicketsByBuildingModel[] = [];

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

  setBuildingName(buildingName: string) {
    console.log(buildingName);
    this.buildingSelected = buildingName;
  }

  showReportByBuilding() {
    this.isVisibleReportBuilding = true;
    this.showReportBuilding();
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


}
