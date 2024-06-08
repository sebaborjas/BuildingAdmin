import { Component } from '@angular/core';
import { LoadingComponent } from '../../loading/loading.component';
import { HotToastService } from '@ngneat/hot-toast';
import { BuildingsService } from '../../services/buildings.service';
import { BuildingModel, CreateBuildingModel, CreateApartmentModel, ManagerModel } from '../../services/types';
import { LoadingService } from '../../services/loading.service';
import { CommonModule, NgFor, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-buildings',
  standalone: true,
  imports: [ CommonModule, FormsModule ],
  templateUrl: './buildings.component.html',
  styleUrl: './buildings.component.css'
})
export class BuildingsComponent {

  buildings: BuildingModel[] = [];
  isVisibleCreateBuildingModal: boolean = false;
  isVisibleAddApartmentModal: boolean = false;
  isVisibleModifyManagerModal: boolean = false;
  createBuildingModel: CreateBuildingModel = {} as CreateBuildingModel;
  createApartmentModel: CreateApartmentModel = {} as CreateApartmentModel;

  apartmentFloor: number = 0;
  apartmentDoorNumber: number = 0;
  apartmentOwnerName: string = "";
  apartmentOwnerLastName: string = "";
  apartmentOwnerEmail: string = "";
  apartmentRooms: number = 0;
  apartmentBathrooms: number = 0;
  apartmentHasTerrace: boolean = false;

  managers: ManagerModel[] = [];

  selectedBuilding: BuildingModel = {} as BuildingModel;
  selectedManager: number = -1;

  
  constructor(private _buildingsService: BuildingsService, private _toastService: HotToastService, private _loadingService: LoadingService, private _userService: UserService) { }

  ngOnInit(){
    this.getBuildings();
    this.getManagers();
    this.createBuildingModel.apartments = [];
  }

  getBuildings(){
    this._loadingService.loadingOn();
    this._buildingsService.getBuildings().subscribe(data => {
      this.buildings = data;
      this._loadingService.loadingOff();

    }, error=>{
      this._loadingService.loadingOff();
    });
  }

  showCreateBuildingModal(){
    this.isVisibleCreateBuildingModal = true;
  }

  hideCreateBuildingModal(){
    this.isVisibleCreateBuildingModal = false;
  }

  showAddApartmentModal(){
    this.isVisibleAddApartmentModal = true;
  }

  hideAddApartmentModal(){
    this.isVisibleAddApartmentModal = false;
  }

  showModifyManager(selectedBuilding: BuildingModel){
    this.isVisibleModifyManagerModal = true;
    if(selectedBuilding.managerName){
      this.selectedManager = this.managers.find(manager => manager.name === selectedBuilding.managerName)?.id || -1;
    }
    this.selectedBuilding = selectedBuilding;
  }

  hideModifyManager(){
    this.isVisibleModifyManagerModal = false;
  }

  addApartment(){
    let newApartment: CreateApartmentModel = {
      floor: this.apartmentFloor,
      doorNumber: this.apartmentDoorNumber,
      ownerName: this.apartmentOwnerName,
      ownerLastName: this.apartmentOwnerLastName,
      ownerEmail: this.apartmentOwnerEmail,
      rooms: this.apartmentRooms,
      bathrooms: this.apartmentBathrooms,
      hasTerrace: this.apartmentHasTerrace
    };
    this.createBuildingModel.apartments.push(newApartment);
  }

  removeApartment(index: number){
    this.createBuildingModel.apartments.splice(index, 1);
  }

  createBuilding(){
    this._buildingsService.createBuilding(this.createBuildingModel).pipe(
      this._toastService.observe({
        loading: 'Creando edificio',
        success: 'Edificio creado correctamente',
        error: 'Error creando edificio',
      })
    ).subscribe(response => {
      this.getBuildings();
    });
  }

  deleteBuilding(id: number){
    this._buildingsService.deleteBuilding(id).pipe(
      this._toastService.observe({
        loading: 'Borrando edificio',
        success: 'Edificio borrado correctamente',
        error: 'Error borrando edificio',
      })
    ).subscribe(response => {
      this.getBuildings();
    });
  }

  getManagers(){
    this._userService.getManagers().subscribe(data => {
      this.managers = data;
    });
  }

  modifyManager(){
    this._buildingsService.modifyManager(this.selectedBuilding.id, this.selectedManager).pipe(
      this._toastService.observe({
        loading: 'Modificando encargado',
        success: 'Encargado modificado correctamente',
        error: 'Error modificando encargado',
      })
    ).subscribe(response => {
      this.getBuildings();
    });
  }

}
