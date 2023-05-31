import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ViewLoanDetailsComponent } from './view-loan-details/view-loan-details.component';
import { ViewOpenLoansComponent } from './view-open-loans/view-open-loans.component';
import { AuthGuard } from '../services/authGuard';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LocationComponent } from './location/location.component';
import { EditLocationComponent } from './edit-location/edit-location.component';
import { PromotionsComponent } from './promotions/promotions.component';


const routes: Routes = [

  {
    
    path: 'advisor/dashboard',
    component: DashboardComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: '',
        component: ViewOpenLoansComponent,
      },
      {
        path: 'view-all-open-loan',
        component: ViewOpenLoansComponent,
        canActivate: [AuthGuard],
      },
      { path: 'view-loan-detail/:id', component: ViewLoanDetailsComponent },
      {
        path: 'add-promotion',
        component: PromotionsComponent,
        canActivate: [AuthGuard],
      },
      { path: 'view-location', component: LocationComponent },
      { path: 'edit-location', component: EditLocationComponent },
    ],
  },


];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  
})
export class AdvisorRoutingModule {}
