import { Component } from '@angular/core';
import { LoadingService } from '../../services/loading.service';
import { NgIf, NgFor, CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AdminService } from '../../services/admin.service';
import { HotToastService } from '@ngneat/hot-toast';
import { RoleTypes } from './roleTypes';

@Component({
  selector: 'app-invitations',
  standalone: true,
  imports: [NgIf, FormsModule, NgFor, CommonModule],
  templateUrl: './invitations.component.html',
  styleUrl: './invitations.component.css',
})
export class InvitationsComponent {
  constructor(
    private _loadingService: LoadingService,
    private _adminService: AdminService,
    private _toastService: HotToastService
  ) {
    this._loadingService.loadingOn();
  }

  modalText = '';

  isVisible = false;
  id = 0;
  email = '';
  name = '';
  rol: number = 1;
  expirationDate = '';
  invitations: any = [];

  showModal() {
    this.isVisible = true;
  }

  hideModal() {
    this.isVisible = false;
  }

  ngOnInit() {
    this.getAllInvitations();
  }

  convertRoleToNumber(rol: string): number {
    if (rol === 'Administrator') {
      return RoleTypes.Administrator;
    }
    return RoleTypes.Manager;
  }

  createInvitationModal() {
    this.modalText = 'Crear Invitacion';
    this.showModal();
  }

  editInvitationModal(invitation: any) {
    this.modalText = 'Editar Invitacion';
    this.id = invitation.id;
    this.email = invitation.email;
    this.name = invitation.name;
    this.rol = this.convertRoleToNumber(invitation.rol);
    this.expirationDate = invitation.expirationDate.split('T')[0];
    this.showModal();
  }

  submitModal() {
    if (this.modalText === 'Crear Invitacion') {
      this.createInvitation();
    } else {
      this.editInvitation();
    }
    this.hideModal();
  }

  createInvitation() {
    this._adminService
      .createInvitation(this.name, this.email, this.rol, this.expirationDate)
      .pipe(
        this._toastService.observe({
          loading: 'Creando invitacion',
          success: 'Invitacion creada con exito',
          error: 'Error al crear invitacion',
        })
      )
      .subscribe(
        (response) => {
          this.getAllInvitations();
        },
        (error) => {
          console.error('Error', error);
        }
      );
  }

  editInvitation() {
    this._adminService
      .editInvitation(this.id, this.expirationDate)
      .pipe(
        this._toastService.observe({
          loading: 'Editando invitacion',
          success: 'Invitacion editada con exito',
          error: 'Error al editar invitacion',
        })
      )
      .subscribe(
        (response) => {
          this.getAllInvitations();
        },
        (error) => {
          console.error('Error', error);
        }
      );
  }

  getAllInvitations() {
    this._loadingService.loadingOn();
    this._adminService.getAllInvitations().subscribe(
      (response) => {
        console.log('Invitations', response);
        this.invitations = response;
      },
      (error) => {
        console.error('Error', error);
      }
    );
    this._loadingService.loadingOff();
  }
}
