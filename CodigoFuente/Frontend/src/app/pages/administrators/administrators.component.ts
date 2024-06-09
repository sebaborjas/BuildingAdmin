import { Component } from '@angular/core';
import { NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HotToastService } from '@ngneat/hot-toast';
import { AdminService } from '../../services/admin.service';




@Component({
  selector: 'app-administrators',
  standalone: true,
  imports: [NgIf, FormsModule],
  templateUrl: './administrators.component.html',
  styleUrl: './administrators.component.css'
})
export class AdministratorsComponent {

  constructor(private _adminService: AdminService, private _toastService: HotToastService) {

  }

  modalVisible: boolean = false;

  email: string = '';
  name: string = '';
  lastName: string = '';
  password: string = '';
  passwordConfirmation: string = '';

  passwordConfirmed: boolean = false;
  emptyFields: boolean = false;

  createAdministratorModal() {
    console.log('createAdministratorModal');
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
    this.checkPasswords();
    if (!this.passwordConfirmed) {
      return;
    }

    this.createAdministartor();
  }

  checkPasswords() {
    this.passwordConfirmed = this.password === this.passwordConfirmation;
    if (!this.passwordConfirmed) {
      this._toastService.error('Las contraseñas no coinciden');
    }
  }

  checkEmptyFields() {
    this.emptyFields = this.email === '' || this.name === '' || this.lastName === '' || this.password === '' || this.passwordConfirmation === '';
    if (this.emptyFields) {
      this._toastService.error('No se admiten campos vacios');
    }
  }

  createAdministartor() {
    this._adminService.createAdministartor(this.name, this.lastName, this.email, this.password)
      .pipe(
        this._toastService.observe({
          loading: 'Creando administrador',
          success: 'Administrador creado con exito',
          error: (e) => e?.error || 'Error al crear el administrador',
        })
      )
      .subscribe(
        (response) => {
          this.hideModal();
        },
        (error) => {
          console.error(error);
        }
      );
  }

}
