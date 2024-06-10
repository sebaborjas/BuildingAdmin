import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { HotToastService } from '@ngneat/hot-toast';
import { InvitationService } from '../services/invitation.service';
import { RejectInvitationModel } from '../services/types';

@Component({
  selector: 'app-manage-invitation',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterLink],
  templateUrl: './manage-invitation.component.html',
  styleUrl: './manage-invitation.component.css'
})
export class ManageInvitationComponent {

  constructor(private _toastService: HotToastService, private _invitationService: InvitationService, private _router: Router) { }
  email: string = '';
  password: string = '';
  passwordConfirmation: string = '';
  modalVisible: boolean = false;

  acceptInvitation() {
    if (!this.areEqualPasswords()) {
      this._toastService.error('Las contraseñas no coinciden');
      return;
    } else if(this.password === '') {
      this._toastService.error('La contraseña es requerida');
      return;
    }
    this.modalVisible = false;
    var acceptInput = {
      email: this.email,
      password: this.password,
    }
    this._invitationService.acceptInvitation(acceptInput).pipe(
      this._toastService.observe({
        loading: 'Aceptando invitación',
        success: 'Invitacion aceptada con exito',
        error: 'Error al aceptar invitacion',
      })
    ).subscribe(() => {
      this._router.navigate(['/login']);
    });
  }

  rejectInvitation() {
    if (this.email === '') {
      this._toastService.error('El email es requerido');
      return;
    }
    var rejectInput: RejectInvitationModel = {
      email: this.email,
    }
    this._invitationService.rejectInvitation(rejectInput).pipe(
      this._toastService.observe({
        loading: 'Rechazando invitación',
        success: 'Invitacion rechazada con exito',
        error: 'Error al rechazar invitacion',
      })
    ).subscribe(() => {
      this._router.navigate(['/login']);
    });
  }

  showModal() {
    if (this.email === '') {
      this._toastService.error('El email es requerido');
    } else {
      this.modalVisible = true;
    }

  }

  hideModal() {
    this.modalVisible = false;
  }

  areEqualPasswords() {
    return this.password === this.passwordConfirmation;
  }

}
