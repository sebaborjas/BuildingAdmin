import { Component } from '@angular/core';
import { UserService } from '../../services/user.service';
import { RoleTypes } from '../invitations/roleTypes';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
  constructor(private _userService: UserService) { }

  name: string = '';
  email: string = '';
  rol: string = '';

  ngOnInit() {
    this._userService.getUser().subscribe((user) => {
      this.name = user.name;
      this.email = user.email;
      this.rol = user.role;
    });
  }
}
