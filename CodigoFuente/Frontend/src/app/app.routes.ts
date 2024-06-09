import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { InvitationsComponent } from './pages/invitations/invitations.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { ReportsAdminComponent } from './pages/reports/admin/reports.component';
import { ReportsManagerComponent } from './pages/reports/manager/reports.component';
import { ConstructionCompanyComponent } from './pages/construction-company/construction-company.component';
import { BuildingsComponent } from './pages/buildings/buildings.component';
import { CompanyAdministratorsComponent } from './pages/company-administrators/company-administrators.component';
import { ImportBuildingsComponent } from './pages/import-buildings/import-buildings.component';
import { TicketsMaintenanceComponent } from './pages/tickets/maintenance/tickets.component';
import { TicketsManagerComponent } from './pages/tickets/manager/tickets.component';
import { AdministratorsComponent } from './pages/administrators/administrators.component';
import { CategoriesComponent } from './pages/categories/categories.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { MaintenanceOperatorsComponent } from './pages/maintenance-operators/maintenance-operators.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: 'home',
    component: HomeComponent,
    children: [
      { path: 'invitations', component: InvitationsComponent },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'reports', 
        children: [
          { path: 'admin', component: ReportsAdminComponent },
          { path: 'manager', component: ReportsManagerComponent}
        ]
      },
      { path: 'constructionCompanies', component: ConstructionCompanyComponent},
      { path: 'buildings', component: BuildingsComponent},
      { path: 'companyAdministrators', component: CompanyAdministratorsComponent},
      { path: 'importBuildings', component: ImportBuildingsComponent},
      { path: 'tickets', 
        children: [
          { path: 'maintenance', component: TicketsMaintenanceComponent },
          { path: 'manager', component: TicketsManagerComponent}
        ]
      },
      { path: 'categories', component: CategoriesComponent },
      { path: 'administrators', component: AdministratorsComponent },
      { path: 'profile', component: ProfileComponent },
      { path: 'maintenanceOperators', component: MaintenanceOperatorsComponent },
    ],
  },
  { path: '**', component: LoginComponent },
];
