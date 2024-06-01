import { Component } from '@angular/core';
import { SectionComponent } from '../section/section.component';

@Component({
  selector: 'app-company-admin',
  standalone: true,
  imports: [SectionComponent],
  templateUrl: './company-admin.component.html',
  styleUrl: './company-admin.component.css',
})
export class CompanyAdminComponent {
  constructor() {}

  selectedSectionIndex: number = -1;

  selectSection(index: number) {
    this.selectedSectionIndex = index;
  }
}
