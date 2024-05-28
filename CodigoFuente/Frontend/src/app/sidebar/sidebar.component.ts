import { Component } from '@angular/core';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})

export class SidebarComponent {
  constructor() { }

  displayDropDown:boolean = false
  showSideBar:boolean = false

  toggleDropDown() {
    this.displayDropDown = !this.displayDropDown
  }

  toggleSideBar() {
    this.showSideBar = !this.showSideBar
  }
}
