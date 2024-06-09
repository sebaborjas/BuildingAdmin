import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { LoadingService } from '../../services/loading.service';
import { HotToastService } from '@ngneat/hot-toast';
import { UserService } from '../../services/user.service';
import { CompanyAdministratorModel, CreateCompanyAdministratorModel } from '../../services/types';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-company-administrators',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './company-administrators.component.html',
  styleUrl: './company-administrators.component.css'
})
export class CompanyAdministratorsComponent implements OnInit{

  constructor(private _toastService: HotToastService, private _loadingService: LoadingService, private _userService: UserService) { }
  
  companyAdministrators: CompanyAdministratorModel[] = [];

  isVisibleNewAdministratorModal: boolean = false;
  administratorNameInput: string = '';
  administratorEmailInput: string = '';
  administratorPasswordInput: string = '';
  
  ngOnInit() {
    this.getCompanyAdministrators();
  };

  showNewAdministratorModal(){
    this.isVisibleNewAdministratorModal = true;
  }

  addAdministrator(){
    var newAdministrator: CreateCompanyAdministratorModel = {
      name: this.administratorNameInput,
      email: this.administratorEmailInput,
      password: this.administratorPasswordInput
    };
    this._userService.createCompanyAdministrator(newAdministrator).pipe(
      this._toastService.observe({
        loading: 'Creando nuevo administrador',
        success: 'Administrador creado correctamente',
        error: 'Error creando administrador',
      })
    ).subscribe(result=>{
      this.getCompanyAdministrators();
    });
  }

  hideBuildingModal(){
    this.isVisibleNewAdministratorModal = false;
  }

  getCompanyAdministrators(){
    this._userService.getCompanyAdministrators().subscribe(result=>{
      this.companyAdministrators = result;
    }, error=>{
      if(error.status === 400){
        this._toastService.error('El usuario no tiene una empresa asignada');
      }
    });
  }
  
}
