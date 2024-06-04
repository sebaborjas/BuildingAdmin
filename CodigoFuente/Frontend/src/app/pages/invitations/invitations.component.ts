import { Component } from '@angular/core';
import { LoadingService } from '../../services/loading.service';
import { NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AdminService } from '../../services/admin.service';
import { HotToastService } from '@ngneat/hot-toast';

@Component({
  selector: 'app-invitations',
  standalone: true,
  imports: [NgIf, FormsModule],
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
  expirationDate = '';

  showModal() {
    this.isVisible = true;
  }

  hideModal() {
    this.isVisible = false;
  }

  ngOnInit() {
    this._loadingService.loadingOff();
  }

  createInvitation() {
    this._adminService
      .createInvitation(this.name, this.email, this.expirationDate)
      .pipe(
        this._toastService.observe({
          loading: 'Creando invitacion',
          success: 'Invitacion creada con exito',
          error: 'Ha ocurrido algun error al crear la invitacion',
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

  // Use templates
}
