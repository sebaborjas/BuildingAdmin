import { NgIf, NgFor } from '@angular/common';
import { Component } from '@angular/core';
import { AdminService } from '../../services/admin.service';
import { HotToastService } from '@ngneat/hot-toast';

@Component({
  selector: 'app-reports',
  standalone: true,
  imports: [NgIf, NgFor],
  templateUrl: './reports.component.html',
  styleUrl: './reports.component.css'
})
export class ReportsComponent {
  constructor(private _adminService: AdminService, private _toastService: HotToastService) { }

  isVisibleReport: boolean = false;
  buildingName: string = 'Casa Migues';
  categoryName: string = '';

  ticketsByCategories: any = [];
  categories: any = [];

  ngOnInit(): void {
    this.isVisibleReport = false;
    this.getCategories();
  }

  showReport() {
    this.isVisibleReport = true;
    this.getTicketsByCategories();
  }

  setCategoryName(categoryName: string) {
    this.categoryName = categoryName;
    console.log(this.categoryName);

  }

  getTicketsByCategories() {
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
      },
        (error) => {
          console.log(error);
        }
      );
  }

  getCategories() {
    this._adminService.getCategories()
      .subscribe((response) => {
        this.categories = response;
      },
        (error) => {
          console.log(error);
        }
      );
  }
}
