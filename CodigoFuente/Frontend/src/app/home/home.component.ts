import { Component } from '@angular/core';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [SidebarComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {
  constructor(private _userService: UserService, private _router: Router) {
    this.getUser();
  }

  name: string = '';
  email: string = '';
  role: string = '';

  getUser() {
    this._userService.getUser().subscribe(
      (response) => {
        if (!response) {
          this._router.navigate(['/login']);
          return;
        }
        this.name = response.name;
        this.email = response.email;
        this.role = response.role;
        console.log(response.name, response.email);
      },
      (error) => {
        console.error('Error', error);
      }
    );
  }
}
