import { NgIf, NgFor } from '@angular/common';
import { Component } from '@angular/core';
import { AdminService } from '../../services/admin.service';
import { HotToastService } from '@ngneat/hot-toast';
import { LoadingService } from '../../services/loading.service';


@Component({
  selector: 'app-reports',
  standalone: true,
  imports: [NgIf, NgFor],
  templateUrl: './reports.component.html',
  styleUrl: './reports.component.css'
})
export class ReportsComponent {
  constructor(
    private _adminService: AdminService,
    private _toastService: HotToastService,
    private _loadingService: LoadingService,

  ) { }

  isVisibleReport: boolean = false;
  buildingName: string = '';
  categoryName: string = '';

  ticketsByCategories: any = [];
  categories: any = [];
  buildings: any = [];

  ngOnInit(): void {
    this.isVisibleReport = false;
    this.getCategories();
    this.getBuilings();
  }

  showReport() {
    if (this.buildingName == '') {
      this._toastService.error('Debe seleccionar un edificio');
      return;
    }
    this.isVisibleReport = true;
    this.getTicketsByCategories();
  }

  setCategoryName(categoryName: string) {
    this.categoryName = categoryName;
  }

  setBuildingName(buildingName: string) {
    this.buildingName = buildingName;
  }

  getTicketsByCategories() {
    this._loadingService.loadingOn();
    this._adminService.getReportTicketsByCategories(this.buildingName, this.categoryName)
      .pipe(
        this._toastService.observe({
          loading: 'Obteniendo reporte',
          success: 'Reporte generado con exito',
          error: (e) => e.message || 'Ha ocurrido un error',
        })
      )
      .subscribe((response) => {
        this.ticketsByCategories = response;
        this._loadingService.loadingOff();
      },
        (error) => {
          console.log(error);
          this._loadingService.loadingOff();
        }

      );
  }

  getBuilings() {
    this._loadingService.loadingOn();
    this._adminService.getBuildings()
      .subscribe((response) => {
        this.buildings = response;
        this._loadingService.loadingOff();
      },
        (error) => {
          console.log(error);
          this._loadingService.loadingOff();
        }
      );
  }

  getCategories() {
    this._loadingService.loadingOn();
    this._adminService.getCategories()
      .subscribe((response) => {
        this.categories = response;
        this._loadingService.loadingOff();
      },
        (error) => {
          console.log(error);
          this._loadingService.loadingOff();
        }
      );
  }
}
