import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ViewOpenLoansComponent } from './advisor/view-open-loans/view-open-loans.component';

// const routes: Routes = [{path:"",component:ViewOpenLoansComponent}];
import { LoginComponent } from './core/login/login.component';
import { UpdatePasswordComponent } from './shared/update-password/update-password.component';
import { AuthGuard } from './services/authGuard';
import { HomeComponent } from './core/home/home.component';

import { UserModule } from './user/user.module';
const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: 'updatePassword',
    component: UpdatePasswordComponent,
    canActivate: [AuthGuard],
  },
  { path: '', component: HomeComponent },
  {
    path: 'viewOpenLoan',
    component: ViewOpenLoansComponent,
    canActivate: [AuthGuard],
  },

  {
    path: 'users',
    loadChildren: () =>
      import('./user/user.module').then((mod) => mod.UserModule),
  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
