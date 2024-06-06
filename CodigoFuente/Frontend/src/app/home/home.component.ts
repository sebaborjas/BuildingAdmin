import { Component } from '@angular/core';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { UserService } from '../services/user.service';
import { Router, RouterOutlet } from '@angular/router';
import { LoadingComponent } from '../loading/loading.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [SidebarComponent, RouterOutlet, LoadingComponent],
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
      },
      (error) => {
        console.error('Error', error);
      }
    );
  }
}
