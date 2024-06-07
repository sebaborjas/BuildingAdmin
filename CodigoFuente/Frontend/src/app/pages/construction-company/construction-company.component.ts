import { Component } from '@angular/core';
import { NgIf } from '@angular/common';
import { ConstructionCompanyService } from '../../services/construction-company.service';

@Component({
  selector: 'app-construction-company',
  standalone: true,
  imports: [ 
    NgIf
  ],
  templateUrl: './construction-company.component.html',
  styleUrl: './construction-company.component.css'
})
export class ConstructionCompanyComponent {
  constructor(private _constructionCompanyService: ConstructionCompanyService) { }

  userHasCompany: boolean = false;
  companyName: string = "";
  companyId: number = -1;
  
  ngOnInit() {
    this.getUserCompany();
  }
  
  saveCompany(name: string) {
    this._constructionCompanyService.saveConstructionCompany(name).subscribe((data) => {
      this.getUserCompany();
      this.userHasCompany = true;
    });
  }

  getUserCompany(){
    this._constructionCompanyService.getConstructionCompany().subscribe(data => {
      this.companyId = data.id;
      this.companyName = data.name;
      this.userHasCompany = true;
    }, (error) => {
      this.userHasCompany = false;
      console.log("User has no company");
      console.log(error)
    });
  }

}
