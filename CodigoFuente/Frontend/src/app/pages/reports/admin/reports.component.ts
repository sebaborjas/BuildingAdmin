import { NgIf, NgFor } from '@angular/common';
import { Component } from '@angular/core';
import { AdminService } from '../../../services/admin.service';
import { BuildingsService } from '../../../services/buildings.service';
import { CategoryService } from '../../../services/category.service';

import { HotToastService } from '@ngneat/hot-toast';
import { LoadingService } from '../../../services/loading.service';
import { CategoryModel, BuildingModel, TicketsByCategoriesModel } from '../../../services/types';
import { ReportService } from '../../../services/report.service';

@Component({
  selector: 'app-admin-reports',
  standalone: true,
  imports: [NgIf, NgFor],
  templateUrl: './reports.component.html',
  styleUrl: './reports.component.css'
})
export class ReportsAdminComponent {
  constructor(
    private _adminService: AdminService,
    private _buildingService: BuildingsService,
    private _toastService: HotToastService,
    private _loadingService: LoadingService,
    private _categoryService: CategoryService,
    private _reportService: ReportService

  ) { }

  isVisibleReport: boolean = false;
  buildingName: string = '';
  categoryName: string = '';

  ticketsByCategories: TicketsByCategoriesModel[] = [];
  categories: CategoryModel[] = [];
  buildings: BuildingModel[] = [];

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

  setCategoryName(event: Event) {
    const categoryName = (event.target as HTMLSelectElement).value;
    this.categoryName = categoryName;
  }

  setBuildingName(event: Event) {
    const buildingName = (event.target as HTMLSelectElement).value;
    this.buildingName = buildingName;
  }

  getTicketsByCategories() {
    this._loadingService.loadingOn();
    this._reportService.getReportTicketsByCategories(this.buildingName, this.categoryName)
      .pipe(
        this._toastService.observe({
          loading: 'Obteniendo reporte',
          success: 'Reporte generado con exito',
          error: (e) => e.message || 'Ha ocurrido un error',
        })
      )
      .subscribe((response: TicketsByCategoriesModel[]) => {
        this.ticketsByCategories = response;
        this._loadingService.loadingOff();
      },
        (error) => {
          this._loadingService.loadingOff();
        }

      );
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

  getCategories() {
    this._loadingService.loadingOn();
    this._categoryService.getCategories()
      .subscribe((response) => {
        this.categories = response;
        this._loadingService.loadingOff();
      },
        (error) => {
          this._loadingService.loadingOff();
        }
      );
  }
}
