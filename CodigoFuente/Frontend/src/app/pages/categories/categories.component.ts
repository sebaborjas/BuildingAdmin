import { Component } from '@angular/core';
import { HotToastService } from '@ngneat/hot-toast';
import { AdminService } from '../../services/admin.service';
import { FormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';


@Component({
  selector: 'app-categories',
  standalone: true,
  imports: [FormsModule, NgIf],
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.css'
})
export class CategoriesComponent {

  constructor(private _adminService: AdminService, private _toastService: HotToastService) { }

  modalVisible: boolean = false;
  name: string = '';
  emptyFields: boolean = false;

  createCategoryModal() {
    this.modalVisible = true;
  }

  hideModal() {
    this.modalVisible = false;
  }

  submitModal() {
    this.checkEmptyFields();
    if (this.emptyFields) {
      return;
    }

    this.createCategory();
  }

  checkEmptyFields() {
    this.emptyFields = this.name === '';
    if (this.emptyFields) {
      this._toastService.error('No se admiten campos vacios');
    }
  }

  createCategory() {
    this._adminService.createCategory(this.name)
      .pipe(
        this._toastService.observe({
          loading: 'Creando categoria',
          success: 'Categoria creada con exito',
          error: (e) => e?.error || 'Error al crear categoria',
        })
      )
      .subscribe(
        (response) => {
          this.hideModal();
        },
        (error) => {
          console.log(error);

        }
      );
  }
}
