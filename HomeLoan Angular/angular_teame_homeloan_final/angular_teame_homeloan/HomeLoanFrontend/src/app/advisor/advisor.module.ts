import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';

import { AdvisorRoutingModule } from './advisor-routing.module';
import { RegisterComponent } from './register/register.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ViewOpenLoansComponent } from './view-open-loans/view-open-loans.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { ViewLoanDetailsComponent } from './view-loan-details/view-loan-details.component';
import { EndpointService } from '../services/endpoint.service';
import { GetAllLoanByadvisorService } from '../services/get-all-loan-byadvisor.service';
import { ViewLoanDetailService } from '../services/view-loan-detail.service';
import { PromotionsComponent } from './promotions/promotions.component';
import { LocationComponent } from './location/location.component';
import { EditLocationComponent } from './edit-location/edit-location.component';
import { GetAllCollateralService } from '../services/get-all-collateral.service';
import { EditCollateralService } from '../services/edit-collateral.service';
import { GetLocationService } from '../services/get-location.service';
import { EditLocationService } from '../services/edit-location.service';

@NgModule({
  declarations: [
    RegisterComponent,
    DashboardComponent,
    ViewOpenLoansComponent,
    ViewLoanDetailsComponent,
    PromotionsComponent,
    LocationComponent,
    EditLocationComponent
  ],
  imports: [
    CommonModule,
    AdvisorRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    MatPaginatorModule,
    MatTableModule,
    MatInputModule,
    MatCardModule,
    BrowserAnimationsModule,
    MatFormFieldModule,
    MatButtonModule,
  ],
  exports: [
    RegisterComponent,
    ViewOpenLoansComponent,
    ViewLoanDetailsComponent,
  ],

  providers:[
    GetAllLoanByadvisorService,
    ViewLoanDetailService,
    GetAllCollateralService,
    EditCollateralService,
    GetLocationService,
    EditLocationService,
    DatePipe

  ]

})
export class AdvisorModule {}
