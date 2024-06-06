import { Component } from '@angular/core';
import { LoadingService } from '../../services/loading.service';
import { NgIf, NgFor, CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AdminService } from '../../services/admin.service';
import { HotToastService } from '@ngneat/hot-toast';

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

  isVisible = false;
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
          console.log('Invitation created', response);
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
