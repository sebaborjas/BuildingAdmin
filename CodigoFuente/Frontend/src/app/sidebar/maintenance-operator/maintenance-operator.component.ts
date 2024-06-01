import { Component } from '@angular/core';
import { SectionComponent } from '../section/section.component';

@Component({
  selector: 'app-maintenance-operator',
  standalone: true,
  imports: [SectionComponent],
  templateUrl: './maintenance-operator.component.html',
  styleUrl: './maintenance-operator.component.css',
})
export class MaintenanceOperatorComponent {
  constructor() {}

  selectedSectionIndex: number = -1;

  selectSection(index: number) {
    this.selectedSectionIndex = index;
  }
}
