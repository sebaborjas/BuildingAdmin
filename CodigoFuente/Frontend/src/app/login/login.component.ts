import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { SesionStorageService } from '../services/sesion-storage.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  email: string = '';
  password: string = '';

  constructor(private _authService: AuthService, private _sesionStorageService: SesionStorageService, private _router: Router) { }

  login() {
    this._authService.login(this.email, this.password).subscribe(
      (response) => {
        this._sesionStorageService.setToken(response.token);
        this._router.navigate(['/home/dahsboard']);
      },
      (error) => {
        console.error('Error', error);
      }
    );
  }
}