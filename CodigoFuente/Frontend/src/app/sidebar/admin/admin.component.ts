import { Component } from '@angular/core';
import { SectionComponent } from '../section/section.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [SectionComponent],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css',
})
export class AdminComponent {
  constructor(private _router: Router) {}

  selectedSectionIndex: number = -1;

  selectSection(index: number) {
    this.selectedSectionIndex = index;
  }

  navigateToSection(section: string) {
    this._router.navigate(['home', section]);
  }
}
