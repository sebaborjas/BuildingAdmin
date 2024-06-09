import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConstructionCompanyService } from '../../services/construction-company.service';
import { LoadingService } from '../../services/loading.service';
import { HotToastService } from '@ngneat/hot-toast';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-construction-company',
  standalone: true,
  imports: [ 
    CommonModule,
    FormsModule
  ],
  templateUrl: './construction-company.component.html',
  styleUrl: './construction-company.component.css'
})
export class ConstructionCompanyComponent {
  constructor(private _constructionCompanyService: ConstructionCompanyService, private _loadingService: LoadingService, private _toastService: HotToastService) { }

  userHasCompany: boolean = false;
  companyName: string = "";
  companyId: number = -1;
  isEditing: boolean = false;
  isVisibleModal: boolean = false;
  
  ngOnInit() {
    this.getUserCompany();
  }

  showEditModal() {
    this.isVisibleModal = true;
  }

  cancelEdit() {
    this.isVisibleModal = false;
  }

  saveEditedCompany() {
    this.isVisibleModal = false;
    this.modifyCompany();
  }

  saveCompany(name: string) {
    this._constructionCompanyService.saveConstructionCompany(name).pipe(
      this._toastService.observe({
        loading: 'Creando empresa',
        success: 'Empresa creada exitosamente',
        error: (e) => e?.error || 'Error creando empresa',
      })
    ).subscribe((data) => {
      this.getUserCompany();
      this.userHasCompany = true;
    });
  }

  getUserCompany(){
    this._loadingService.loadingOn();
    this._constructionCompanyService.getConstructionCompany().subscribe(data => {
      this.companyId = data.id;
      this.companyName = data.name;
      this.userHasCompany = true;
      this._loadingService.loadingOff();
    }, (error) => {
      this.userHasCompany = false;
      this._loadingService.loadingOff();
    });
  }

  editUserCompany(){
    this.isEditing = true;
  }

  modifyCompany(){
    this._constructionCompanyService.modifyConstructionCompany(this.companyName).pipe(
      this._toastService.observe({
        loading: 'Modificando empresa',
        success: 'Empresa modificada exitosamente',
        error: (e) => e?.error || 'Error modificando empresa',
      })
    ).subscribe(result => {
      this.getUserCompany();
      this.isEditing = false;
    });
  }

}
