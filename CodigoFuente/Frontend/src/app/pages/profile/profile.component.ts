import { Component } from '@angular/core';
import { UserService } from '../../services/user.service';
import { LoadingService } from '../../services/loading.service';



@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {


  constructor(private _userService: UserService, private _loadingService: LoadingService) { }

  name: string = '';
  email: string = '';
  rol: string = '';

  ngOnInit() {
    this._loadingService.loadingOn();
    this._userService.getUser().subscribe((user) => {
      this.name = user.name;
      this.email = user.email;
      this.rol = user.role;
      this._loadingService.loadingOff();
    });
  }

}
