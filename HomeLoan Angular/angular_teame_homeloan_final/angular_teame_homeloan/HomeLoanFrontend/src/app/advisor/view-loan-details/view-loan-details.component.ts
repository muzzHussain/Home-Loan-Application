import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdvisorService } from 'src/app/services/advisorService';
import { DatePipe } from '@angular/common';
@Component({
  selector: 'app-view-loan-details',
  templateUrl: './view-loan-details.component.html',
  styleUrls: ['./view-loan-details.component.css'],
})
export class ViewLoanDetailsComponent {
  applicationID: string;
  emailofUser: string;
  alert: boolean = false;
  alertClass: string = '';
  successMessage: string = '';
  errorMessage: string = '';
  loanDetail: any;
  loanDetailForm: FormGroup;
  disabledAccepted: boolean = false;
  disabledRecommended: boolean = false;

  constructor(
    private router: ActivatedRoute,
    public advisorService: AdvisorService,
    private fb: FormBuilder,
    private route: Router,
    private date: DatePipe
  ) {}

  async ngOnInit() {
    this.loanDetailForm = this.fb.group({
      id: [''],
      emailId: [''],
      address: [''],
      size: [''],
      cost: [''],
      registrationCost: [''],
      monthlyFamilyIncome: [''],
      otherIncome: [''],
      loanAmount: [''],
      loanDuration: [''],
      loanStartDate: [''],
      status: [''],
      notes: [''],
    });

    this.applicationID = this.router.snapshot.paramMap.get('id');
    this.fetchLoanDetail();
    // this.disableButtons();
  }

  disableFormControls(): void {
    Object.keys(this.loanDetailForm.controls).forEach((controlName) => {
      if (controlName != 'notes') {
        this.loanDetailForm.get(controlName).disable();
      }
    });
  }

  patchValueForm(): void {
    this.loanDetailForm.patchValue({
      id: this.loanDetail.id,
      emailId: this.loanDetail.emailId,
      address: this.loanDetail.address,
      size: this.loanDetail.size,
      cost: this.loanDetail.cost,
      registrationCost: this.loanDetail.registrationCost,
      monthlyFamilyIncome: this.loanDetail.monthlyFamilyIncome,
      otherIncome: this.loanDetail.otherIncome,
      loanAmount: this.loanDetail.loanAmount,
      loanDuration: this.loanDetail.loanDuration,
      loanStartDate: this.date.transform(
        this.loanDetail.loanStartDate,
        'dd-MM-yyyy'
      ),
      status: this.loanDetail.status,
    });
    this.disableFormControls();
    this.disableButtons();
  }

  async fetchLoanDetail() {
    this.loanDetail = await this.advisorService
      .getAllLoansApplicationDetails(this.applicationID)
      .then((loanDetail) => {
        this.loanDetail = loanDetail;
        this.patchValueForm();
        this.disableButtons();
      });
  }

  async updateStatus(status: string) {
    try {
      const resp = await this.advisorService.changeStatusOfAppliedLoans(
        this.applicationID,
        status,
        this.loanDetailForm.value.notes
      );

      if (resp.toString() === 'true') {
        this.alertClass = 'alert alert-success';
        this.alert = true;
        this.successMessage = 'Loan status changed successfully';
        setTimeout(() => {
          this.alert = false;
          this.fetchLoanDetail();
        }, 1000);
      }
    } catch (error) {
      this.alertClass = 'alert alert-danger';
      this.alert = true;
      this.errorMessage = 'Error in changing status';
    }
  }

  async disableButtons() {
    if (this.loanDetail && this.loanDetail.status === 'Applied') {
      this.disabledAccepted = true;
    } else if (this.loanDetail && this.loanDetail.status === 'InProgress') {
      this.disabledRecommended = true;
    }
  }

  back() {
    this.route.navigateByUrl('advisor/dashboard');
  }

  closeAlert() {
    this.alert = false;
  }
}
