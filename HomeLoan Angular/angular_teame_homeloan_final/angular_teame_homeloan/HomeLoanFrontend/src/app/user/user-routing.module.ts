import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateLoanComponent } from './create-loan/create-loan.component';
import { AuthGuard } from '../services/authGuard';
import { RegUserComponent } from './reg-user/reg-user.component';
import { AddCollateralComponent } from './add-collateral/add-collateral.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { EditLoanComponent } from './edit-loan/edit-loan.component';
import { ViewCollateralComponent } from './view-collateral/view-collateral.component';
import { EditCollateralComponent } from './edit-collateral/edit-collateral.component';

const routes: Routes = [ {
  path: 'add-loan',
  component: CreateLoanComponent,
  canActivate: [AuthGuard],
},
{
  path: 'edit-loan',
  component: EditLoanComponent,
  canActivate: [AuthGuard],
},
{
  path: 'add-collateral',
  component: AddCollateralComponent,
  canActivate: [AuthGuard],
},
{
  path: 'dashboard',
  component: DashboardComponent,
  canActivate: [AuthGuard],
},
{path: 'register',
component: RegUserComponent,
},
{path: 'view-collateral',
component: ViewCollateralComponent,
},
{path: 'edit-collateral/:id/:type/:value/:share',
component: EditCollateralComponent,
},
{
  path:"",
  loadChildren:()=>import('../shared/shared.module').then(m=>m.SharedModule)
},
];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
