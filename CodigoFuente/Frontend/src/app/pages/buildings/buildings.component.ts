import { Component } from '@angular/core';
import { LoadingComponent } from '../../loading/loading.component';
import { HotToastService } from '@ngneat/hot-toast';
import { BuildingsService } from '../../services/buildings.service';
import { BuildingModel, CreateBuildingModel, ApartmentModel, ManagerModel, ModifyApartmentModel, ModifyBuildingModel } from '../../services/types';
import { LoadingService } from '../../services/loading.service';
import { CommonModule, NgFor, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-buildings',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './buildings.component.html',
  styleUrl: './buildings.component.css'
})
export class BuildingsComponent {

  buildings: BuildingModel[] = [];
  managers: ManagerModel[] = [];

  isVisibleBuildingModal: boolean = false;
  isVisibleApartmentModal: boolean = false;
  isVisibleModifyManagerModal: boolean = false;
  isEditingBuilding: boolean = false;
  isEditingApartment: boolean = false;
  isVisibleDeleteConfirmation: boolean = false;

  buildingNameInput: string = '';
  buildingAddressInput: string = '';
  buildingLocationInput: string = '';
  buildingExpensesInput: number = 0;
  buildingApartmentsInput: ApartmentModel[] = [];
  buildingManagerEmailInput: string = '';
  createBuildingModel: CreateBuildingModel = {} as CreateBuildingModel;

  apartmentFloorInput: number = 0;
  apartmentDoorNumberInput: number = 0;
  apartmentOwnerNameInput: string = '';
  apartmentOwnerLastNameInput: string = '';
  apartmentOwnerEmailInput: string = '';
  apartmentRoomsInput: number = 0;
  apartmentBathroomsInput: number = 0;
  apartmentHasTerraceInput: boolean = false;
  createApartmentModel: ApartmentModel = {} as ApartmentModel;

  selectedBuilding: BuildingModel = {} as BuildingModel;
  selectedManager: number = -1;
  selectedApartment: ApartmentModel = {} as ApartmentModel;

  constructor(private _buildingsService: BuildingsService, private _toastService: HotToastService, private _loadingService: LoadingService, private _userService: UserService) { }

  ngOnInit() {
    this.getBuildings();
    this.getManagers();
    this.createBuildingModel.apartments = [];
  }

  getBuildings() {
    this._loadingService.loadingOn();
    this._buildingsService.getBuildings().subscribe(data => {
      this.buildings = data;
      this._loadingService.loadingOff();

    }, error => {
      this._loadingService.loadingOff();
    });
  }

  showDeleteConfirmation(building: BuildingModel) {
    this.selectedBuilding = building;
    this.isVisibleDeleteConfirmation = true;
  }

  deleteBuildingConfirmed() {
    this.deleteBuilding(this.selectedBuilding.id);
    this.isVisibleDeleteConfirmation = false;
  }

  cancelDelete() {
    this.isVisibleDeleteConfirmation = false;
  }

  showBuildingModal(editing: boolean, building?: BuildingModel) {
    this.isEditingBuilding = editing;
    if (building) {
      this.selectedBuilding = building;
      this.buildingNameInput = building.name;
      this.buildingAddressInput = building.address;
      this.buildingLocationInput = building.location;
      this.buildingExpensesInput = building.expenses;
      this.buildingApartmentsInput = building.apartments;
      this.buildingManagerEmailInput = building.managerName || '';

    }
    this.isVisibleBuildingModal = true;
  }

  hideBuildingModal() {
    this.isVisibleBuildingModal = false;
  }

  showApartmentModal(editing: boolean, apartment?: ApartmentModel) {
    this.isEditingApartment = editing;
    if (apartment) {
      this.selectedApartment = apartment;
      this.apartmentDoorNumberInput = apartment.doorNumber;
      this.apartmentFloorInput = apartment.floor;
      this.apartmentOwnerEmailInput = apartment.ownerEmail;
      this.apartmentOwnerLastNameInput = apartment.ownerLastName;
      this.apartmentOwnerNameInput = apartment.ownerName;
      this.apartmentRoomsInput = apartment.rooms;
      this.apartmentBathroomsInput = apartment.bathrooms;
      this.apartmentHasTerraceInput = apartment.hasTerrace;
    }
    this.isVisibleApartmentModal = true;

  }

  hideApartmentModal() {
    this.isVisibleApartmentModal = false;
  }

  showModifyManager(selectedBuilding: BuildingModel) {
    this.isVisibleModifyManagerModal = true;
    if (selectedBuilding.managerName) {
      this.selectedManager = this.managers.find(manager => manager.name === selectedBuilding.managerName)?.id || -1;
    }
    this.selectedBuilding = selectedBuilding;
  }

  hideModifyManager() {
    this.isVisibleModifyManagerModal = false;
  }

  addApartment() {
    let newApartment: ApartmentModel = {
      floor: this.apartmentFloorInput,
      doorNumber: this.apartmentDoorNumberInput,
      ownerName: this.apartmentOwnerNameInput,
      ownerLastName: this.apartmentOwnerLastNameInput,
      ownerEmail: this.apartmentOwnerEmailInput,
      rooms: this.apartmentRoomsInput,
      bathrooms: this.apartmentBathroomsInput,
      hasTerrace: this.apartmentHasTerraceInput
    };
    this.buildingApartmentsInput.push(newApartment);
  }

  removeApartment(index: number) {
    this.buildingApartmentsInput.splice(index, 1);
  }

  createBuilding() {
    this.createBuildingModel = {
      name: this.buildingNameInput,
      address: this.buildingAddressInput,
      location: this.buildingLocationInput,
      expenses: this.buildingExpensesInput,
      apartments: this.buildingApartmentsInput,
      managerEmail: this.buildingManagerEmailInput
    }
    this._buildingsService.createBuilding(this.createBuildingModel).pipe(
      this._toastService.observe({
        loading: 'Creando edificio',
        success: 'Edificio creado correctamente',
        error: (e) => e?.error || 'Error creando edificio',
      })
    ).subscribe(result => {
      this.getBuildings();
    });
  }

  deleteBuilding(id: number) {
    this._buildingsService.deleteBuilding(id).pipe(
      this._toastService.observe({
        loading: 'Borrando edificio',
        success: 'Edificio borrado correctamente',
        error: (e) => e?.error || 'Error borrando edificio',
      })
    ).subscribe(() => {
      this.getBuildings();
    });
  }

  modifyBuilding() {
    let modifyApartmentsInput: ModifyApartmentModel[] = [];
    this.buildingApartmentsInput.forEach(apartment => {
      modifyApartmentsInput.push({
        id: apartment.id || -1,
        ownerEmail: apartment.ownerEmail,
        ownerLastName: apartment.ownerLastName,
        ownerName: apartment.ownerName
      });
    });
    let modifyBuildingInput: ModifyBuildingModel = {
      expenses: this.buildingExpensesInput,
      apartments: modifyApartmentsInput
    }
    let buildingId = this.selectedBuilding.id;
    this._buildingsService.modifyBuilding(modifyBuildingInput, buildingId).pipe(
      this._toastService.observe({
        loading: 'Modificando edificio',
        success: 'Edificio modificado correctamente',
        error: (e) => e?.error || 'Error modificando edificio',
      })
    ).subscribe(() => {
      this.getBuildings();
    })
  }

  modifyApartment() {
    this.selectedApartment.ownerEmail = this.apartmentOwnerEmailInput;
    this.selectedApartment.ownerLastName = this.apartmentOwnerLastNameInput;
    this.selectedApartment.ownerName = this.apartmentOwnerNameInput;
  }

  getManagers() {
    this._userService.getManagers().subscribe(data => {
      this.managers = data;
    });
  }

  modifyManager() {
    this._buildingsService.modifyManager(this.selectedBuilding.id, this.selectedManager).pipe(
      this._toastService.observe({
        loading: 'Modificando encargado',
        success: 'Encargado modificado correctamente',
        error: (e) => e?.error || 'Error modificando encargado',
      })
    ).subscribe(() => {
      this.getBuildings();
    });
  }

}
