import { Component, Input } from '@angular/core';
import { SectionComponent } from './section/section.component';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [SectionComponent],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css',
})
export class SidebarComponent {
  @Input() name?: string;
  @Input() email?: string;
  @Input() role?: string;

  constructor() {}

  displayDropDown: boolean = false;
  showSideBar: boolean = false;

  toggleDropDown() {
    this.displayDropDown = !this.displayDropDown;
  }

  toggleSideBar() {
    this.showSideBar = !this.showSideBar;
  }
}
