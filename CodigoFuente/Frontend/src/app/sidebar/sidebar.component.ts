import { Component, Input } from '@angular/core';
import { roles } from './roleTypes';
import { NgIf, NgClass } from '@angular/common';
import { SesionStorageService } from '../services/sesion-storage.service';
import { Router, RouterLink } from '@angular/router';


@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [
    NgIf,
    NgClass,
    RouterLink
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

  selectedSectionIndex: number = -1;

  selectSection(index: number) {
    this.selectedSectionIndex = index;
  }

  toggleDropDown() {
    this.displayDropDown = !this.displayDropDown;
  }

  toggleSideBar() {
    this.showSideBar = !this.showSideBar;
  }

  hideSideBar() {
    this.showSideBar = false;
  }

  hideDropDown() {
    this.displayDropDown = false;
  }

  logout() {
    this._sessionStorageService.removeToken();
    this._router.navigate(['/login']);
  }
}
