import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { InvitationsComponent } from './pages/invitations/invitations.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: 'home',
    component: HomeComponent,
    children: [{ path: 'invitations', component: InvitationsComponent }],
  },
  { path: '**', component: LoginComponent },
];
