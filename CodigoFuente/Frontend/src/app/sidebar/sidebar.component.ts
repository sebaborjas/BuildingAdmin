import { Component, Input } from '@angular/core';
import { AdminComponent } from './admin/admin.component';
import { ManagerComponent } from './manager/manager.component';
import { MaintenanceOperatorComponent } from './maintenance-operator/maintenance-operator.component';
import { CompanyAdminComponent } from './company-admin/company-admin.component';
import { roles } from './roleTypes';
import { NgIf } from '@angular/common';
import { SesionStorageService } from '../services/sesion-storage.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [
    AdminComponent,
    ManagerComponent,
    NgIf,
    MaintenanceOperatorComponent,
    CompanyAdminComponent,
  ],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css',
})
export class SidebarComponent {
  @Input() name?: string;
  @Input() email?: string;
  @Input() role?: string;

  admin = roles.ADMINISTRATOR;
  manager = roles.MANAGER;
  maintenanceOperator = roles.MAINTENANCE_OPERATOR;
  companyAdmin = roles.COMPANY_ADMIN;

  constructor(private _sessionStorageService: SesionStorageService, private _router: Router) { }

  displayDropDown: boolean = false;
  showSideBar: boolean = false;

  toggleDropDown() {
    this.displayDropDown = !this.displayDropDown;
  }

  toggleSideBar() {
    this.showSideBar = !this.showSideBar;
  }

  navigateToProfile() {
    this._router.navigate(['/profile']);
  }

  logout() {
    this._sessionStorageService.removeToken();
    this._router.navigate(['/login']);
  }
}
