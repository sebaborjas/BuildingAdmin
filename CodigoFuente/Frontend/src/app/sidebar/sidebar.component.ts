import { Component, Input } from '@angular/core';
import { roles } from './roleTypes';
import { NgClass, NgIf } from '@angular/common';
import { Router } from '@angular/router';


@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [
    NgIf,
    NgClass,
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

  constructor(private _router: Router) { }

  displayDropDown: boolean = false;
  showSideBar: boolean = false;

  selectedSectionIndex: number = -1;

  selectSection(index: number) {
    this.selectedSectionIndex = index;
  }

  navigateTo(section: string) {
    this._router.navigate(['home', section]);
  }

  toggleDropDown() {
    this.displayDropDown = !this.displayDropDown;
  }

  toggleSideBar() {
    this.showSideBar = !this.showSideBar;
  }

  goHome(){
    this._router.navigate(['home']);
  }
}
