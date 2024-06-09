import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { InvitationsComponent } from './pages/invitations/invitations.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { ReportsComponent } from './pages/reports/reports.component';
import { ConstructionCompanyComponent } from './pages/construction-company/construction-company.component';
import { BuildingsComponent } from './pages/buildings/buildings.component';
import { CompanyAdministratorsComponent } from './pages/company-administrators/company-administrators.component';
import { ImportBuildingsComponent } from './pages/import-buildings/import-buildings.component';
import { TicketsComponent } from './pages/tickets/tickets.component';
import { AdministratorsComponent } from './pages/administrators/administrators.component';
import { CategoriesComponent } from './pages/categories/categories.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { ManageInvitationComponent } from './manage-invitation/manage-invitation.component';
import { AuthGuard } from './guards/auth.guard';
import { RoleGuard } from './guards/role.guard';
import { roles } from './sidebar/roleTypes';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'invitation', component: ManageInvitationComponent },
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [AuthGuard],
    children: [
      { path: 'dashboard', component: DashboardComponent },
      { path: 'profile', component: ProfileComponent },
      {
        path: 'invitations', component: InvitationsComponent,
        canActivate: [RoleGuard], data: { expectedRole: roles.ADMINISTRATOR }
      },
      {
        path: 'reports', component: ReportsComponent,
        canActivate: [RoleGuard], data: { expectedRole: roles.ADMINISTRATOR }
      },
      {
        path: 'categories', component: CategoriesComponent,
        canActivate: [RoleGuard], data: { expectedRole: roles.ADMINISTRATOR }
      },
      {
        path: 'administrators', component: AdministratorsComponent,
        canActivate: [RoleGuard], data: { expectedRole: roles.ADMINISTRATOR }
      },
      { path: 'constructionCompanies', component: ConstructionCompanyComponent },
      { path: 'buildings', component: BuildingsComponent },
      { path: 'companyAdministrators', component: CompanyAdministratorsComponent },
      { path: 'importBuildings', component: ImportBuildingsComponent },
      { path: 'tickets', component: TicketsComponent, },


    ],
  },
  { path: '**', component: LoginComponent },
];
