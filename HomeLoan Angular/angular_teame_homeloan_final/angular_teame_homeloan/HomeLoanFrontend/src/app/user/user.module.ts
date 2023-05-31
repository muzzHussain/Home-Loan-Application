import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
// import { RegisterComponent } from './register/register.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CreateLoanComponent } from './create-loan/create-loan.component';
import { RegUserComponent} from './reg-user/reg-user.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AddCollateralComponent } from './add-collateral/add-collateral.component';
import { EditLoanComponent } from './edit-loan/edit-loan.component';
// import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ViewCollateralComponent } from './view-collateral/view-collateral.component';
import { GetAllCollateralService } from '../services/get-all-collateral.service';
import {MatPaginatorModule} from '@angular/material/paginator';
// import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatTableModule} from '@angular/material/table';
import { MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import { DeleteCollateralService } from '../services/delete-collateral.service';
import { EditCollateralComponent } from './edit-collateral/edit-collateral.component';


@NgModule({
  declarations: [
    RegUserComponent,
    DashboardComponent,
    CreateLoanComponent,
    AddCollateralComponent,
    EditLoanComponent,
    ViewCollateralComponent,
    EditCollateralComponent
    
    
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    MatPaginatorModule,
      MatTableModule,
      MatFormFieldModule,
      MatInputModule,
      MatButtonModule,
      FormsModule,
      ReactiveFormsModule,
      MatCardModule
  ],
  
  exports:[
    CreateLoanComponent,
   
    
  ],
  providers:[
    GetAllCollateralService,
    DeleteCollateralService
  ]
})
export class UserModule { }
