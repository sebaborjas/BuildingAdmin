import { Component } from '@angular/core';
import { SectionComponent } from '../section/section.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-company-admin',
  standalone: true,
  imports: [SectionComponent],
  templateUrl: './company-admin.component.html',
  styleUrl: './company-admin.component.css',
})
export class CompanyAdminComponent {
  constructor(private _router: Router) {}

  selectedSectionIndex: number = -1;

  selectSection(index: number) {
    this.selectedSectionIndex = index;
  }

  navigateToSection(section: string) {
    this._router.navigate(['home', section]);
  }
}
