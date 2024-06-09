import { Component, OnInit } from '@angular/core';
import { ImporterService } from '../../services/importer.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CreateBuildingOutputModel, ImporterInputModel } from '../../services/types';
import { HotToastService } from '@ngneat/hot-toast';
import { LoadingService } from '../../services/loading.service';


@Component({
  selector: 'app-import-buildings',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './import-buildings.component.html',
  styleUrl: './import-buildings.component.css'
})
export class ImportBuildingsComponent implements OnInit {

  constructor(private _importerService: ImporterService, private _toastService: HotToastService, private _loadingService: LoadingService) { }

  importers: string[] = [];
  filePath: string = '';
  selectedImporter: string = '';
  importedBuildings: CreateBuildingOutputModel[] = [];
  errorBuildings: string[] = [];
  showResults: boolean = false;

  ngOnInit(){
    this.getImporters();
  }

  importBuildings(){
    var importerInput: ImporterInputModel = {
      path: this.filePath,
      importerName: this.selectedImporter
    };
    this._importerService.importBuildings(importerInput).pipe(
      this._toastService.observe({
        loading: 'Importando edificios',
        success: 'Importación completada',
        error: 'Error al importar',
      })
    ).subscribe(result=>{
      console.log(result);
      this.importedBuildings = result.createdBuildings;
      this.errorBuildings = result.errors;
      this.showResults = true;
    });

  }

  getImporters(){
    this._loadingService.loadingOn();
    this._importerService.getImporters().subscribe(result=>{
      this.importers = result;
      if(this.importers.length > 0){
        this.selectedImporter = this.importers[0];
      }
      this._loadingService.loadingOff();
    }, error=>{
      this._loadingService.loadingOff();
    });
  }

  

}
