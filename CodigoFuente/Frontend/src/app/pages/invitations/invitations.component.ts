import { Component } from '@angular/core';
import { LoadingService } from '../../services/loading.service';

@Component({
  selector: 'app-invitations',
  standalone: true,
  imports: [],
  templateUrl: './invitations.component.html',
  styleUrl: './invitations.component.css',
})
export class InvitationsComponent {
  constructor(private loadingService: LoadingService) {
    this.loadingService.loadingOn();
  }

  ngOnInit() {
    this.loadingService.loadingOff();
  }
}
