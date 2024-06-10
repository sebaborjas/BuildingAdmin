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
import { ManageInvitationComponent } from './manage-invitation/manage-invitation.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';
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
      {
        path: 'reports',
        children: [
          {
            path: 'admin', component: ReportsAdminComponent,
            canActivate: [RoleGuard], data: { expectedRole: roles.ADMINISTRATOR }
          },
          {
            path: 'manager', component: ReportsManagerComponent,
            canActivate: [RoleGuard], data: { expectedRole: roles.MANAGER }
          },
        ]
      },
      {
        path: 'constructionCompanies', component: ConstructionCompanyComponent,
        canActivate: [RoleGuard], data: { expectedRole: roles.COMPANY_ADMIN }
      },
      {
        path: 'buildings', component: BuildingsComponent,
        canActivate: [RoleGuard], data: { expectedRole: roles.COMPANY_ADMIN }
      },
      {
        path: 'companyAdministrators', component: CompanyAdministratorsComponent,
        canActivate: [RoleGuard], data: { expectedRole: roles.COMPANY_ADMIN }
      },
      {
        path: 'importBuildings', component: ImportBuildingsComponent,
        canActivate: [RoleGuard], data: { expectedRole: roles.COMPANY_ADMIN }
      },
      {
        path: 'tickets',
        children: [
          {
            path: 'maintenance', component: TicketsMaintenanceComponent,
            canActivate: [RoleGuard], data: { expectedRole: roles.MAINTENANCE_OPERATOR }
          },
          {
            path: 'manager', component: TicketsManagerComponent,
            canActivate: [RoleGuard], data: { expectedRole: roles.MANAGER }
          },
        ]
      },
      {
        path: 'categories', component: CategoriesComponent,
        canActivate: [RoleGuard], data: { expectedRole: roles.ADMINISTRATOR }
      },
      {
        path: 'administrators', component: AdministratorsComponent,
        canActivate: [RoleGuard], data: { expectedRole: roles.ADMINISTRATOR }
      },
      { path: 'profile', component: ProfileComponent },
      {
        path: 'maintenanceOperators', component: MaintenanceOperatorsComponent,
        canActivate: [RoleGuard], data: { expectedRole: roles.MANAGER }
      },
      {
        path: 'invitations', component: InvitationsComponent,
        canActivate: [RoleGuard], data: { expectedRole: roles.ADMINISTRATOR }
      },

    ],
  },
  { path: '**', component: NotFoundComponent },
];
