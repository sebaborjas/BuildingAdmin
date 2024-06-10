import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { LoadingService } from '../../services/loading.service';
import { UserService } from '../../services/user.service';
import { BuildingsService } from '../../services/buildings.service';
import { HotToastService } from '@ngneat/hot-toast';
import { MaintenanceOperatorModel, BuildingModel } from '../../services/types';

@Component({
  selector: 'app-maintenance-operators',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './maintenance-operators.component.html',
  styleUrl: './maintenance-operators.component.css'
})
export class MaintenanceOperatorsComponent {

  constructor(
    private _loadingService: LoadingService,
    private _userService: UserService,
    private _toastService: HotToastService,
    private _buildingService: BuildingsService
  ) { }

  isVisibleModal: boolean = false;
  name: string = '';
  lastName: string = '';
  email: string = '';
  password: string = '';
  selectedBuildings: number[] = [];
  buildings: BuildingModel[] = [];

  ngOnInit(): void {
    this.getBuildings();
  }

  showModal() {
    this.isVisibleModal = true;
  }

  hideModal() {
    this.isVisibleModal = false;
  }

  getBuildings() {
    this._loadingService.loadingOn();
    this._buildingService.getBuildings()
      .pipe(
        this._toastService.observe({
          loading: 'Obteniendo edificios',
          success: 'Edificios obtenidos con exito',
          error: (e) => e?.error || 'Error al obtener edificios'
        })
      )
      .subscribe((response: BuildingModel[]) => {
        this.buildings = response;
        this._loadingService.loadingOff();
      });
  }

  createMaintenanceOperator() {

    var maintenanceOperator: MaintenanceOperatorModel = {
      name: this.name,
      lastName: this.lastName,
      email: this.email,
      password: this.password,
      buildings: this.selectedBuildings
    };

    this._loadingService.loadingOn();
    this._userService.createMaintenanceOperator(maintenanceOperator)
      .pipe(
        this._toastService.observe({
          loading: 'Creando operador de mantenimiento',
          success: 'Operador de mantenimiento creado con exito',
          error: (e) => e?.error || 'Error al crear operador de mantenimiento'
        })
      )
      .subscribe((response) => {
        this._loadingService.loadingOff();
        this.hideModal();
      },
        (error) => {
          this._loadingService.loadingOff();
        });
  }

}
