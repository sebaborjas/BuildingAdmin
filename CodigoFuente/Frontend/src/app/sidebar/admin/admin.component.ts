import { Component, INJECTOR, Input } from '@angular/core';
import { SectionComponent } from '../section/section.component';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [SectionComponent],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css',
})
export class AdminComponent {
  constructor() {}

  selectedSectionIndex: number = -1;

  selectSection(index: number) {
    this.selectedSectionIndex = index;
  }
}
