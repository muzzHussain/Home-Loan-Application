import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CollateralListDto } from 'src/app/models/collateral-list-dto';
import userLoanApplicationDTO from 'src/app/models/userLoanApplicationDTO';
import { CollateralService } from 'src/app/services/collateral.service';
import { LoanApiService } from 'src/app/services/loan-api.service';
import { ModelInitializerService } from 'src/app/services/model-initializer.service';
@Component({
  selector: 'app-user-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent {
  loanList: userLoanApplicationDTO[];

  collateralData: CollateralListDto[];
  selectedCollateralId: string;
  viewParticularLoan: userLoanApplicationDTO;
  loanId: string;

  isEmptyCollateral: boolean;

  constructor(
    private loanApi: LoanApiService,
    private collateralApi: CollateralService,
    modelInitializer: ModelInitializerService,
    private router: Router
  ) {
    this.viewParticularLoan = modelInitializer.userLoanApplication;
    this.isEmptyCollateral = false;
    this.selectedCollateralId = '';
    this.loanId = '';
  }

  ngOnInit(): void {
    this.loanApi.getAllLoans().subscribe({
      next: (response) => {
        this.loanList = response;
      },
      error: (err) => {
        if (err.status === 401) {
          this.router.navigateByUrl('/login');
        } else {
          console.log(err);
        }
      },
    });
  }

  addLoanRoute(): void {
    this.router.navigateByUrl('/users/add-loan');
  }
  addCollateralRoute(): void {
    this.router.navigateByUrl('/users/add-collateral');
  }
  collateralListRoute(): void {
    this.router.navigateByUrl('/users/view-collateral');
  }

  //Apply Loan Modal Methods
  applyLoan(row: userLoanApplicationDTO): void {
    this.loanId = row.id;
    this.loadCollateralData();
  }

  onChangeCollateralData($event) {
    const id: string = $event.target.value;
    const isChecked: boolean = $event.target.checked;

    this.collateralData = this.collateralData.map((collateral) => {
      if (collateral.id === id) {
        collateral.checked = isChecked;
      }
      return collateral;
    });
  }

  attachCollateral(): Promise<number> {
    return new Promise((resolve, reject) => {
      this.collateralData.forEach((collateral) => {
        if (collateral.checked) {
          this.collateralApi
            .linkCollateralToLoan(this.loanId, collateral.id)
            .subscribe({
              next: (response) => {
                resolve(1);
              },
              error: (err) => {
                console.log(err);
                reject(0);
              },
            });
        }
      });
    });
  }
  submit() {
    if (this.collateralData.some((collateral) => collateral.checked === true)) {
      this.attachCollateral()
        .then(() => {
          this.loanApi.applyLoan(this.loanId).subscribe({
            next: (response) => {
              this.loanList.forEach((loan) => {
                if (loan.id === this.loanId) {
                  loan.status = 'Applied';
                }
              });
            },
            error: (err) => {
              console.log(err);
            },
          });
        })
        .catch(() => {
          console.log('There is problem on linking collateral to loan');
        });
    }
  }

  submitRadio() {
    console.log(this.loanId);
    console.log(this.selectedCollateralId);
    if (this.selectedCollateralId) {
      this.collateralApi
        .linkCollateralToLoan(this.loanId, this.selectedCollateralId)
        .subscribe({
          next: (response) => {
            this.loanApi.applyLoan(this.loanId).subscribe({
              next: (response) => {
                this.loanList.forEach((loan) => {
                  if (loan.id === this.loanId) {
                    loan.status = 'Applied';
                  }
                });
              },
              error: (err) => {
                console.log(err);
              },
            });
          },
          error: (err) => {
            console.log(err);
          },
        });
    }
  }
  private loadCollateralData(): void {
    this.collateralApi.getAllCollaterals().subscribe({
      next: (response) => {
        this.collateralData = response;
        if (this.collateralData.length === 0) {
          this.isEmptyCollateral = true;
        }
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  //View Loan Modal Methods
  viewLoan(row: userLoanApplicationDTO): void {
    this.viewParticularLoan = row;
    console.log(this.viewParticularLoan);
  }

  //Edit Loan Button
  editLoan(row: userLoanApplicationDTO): void {
    this.router.navigate(['users/edit-loan'], { state: { data: row } });
  }
}
