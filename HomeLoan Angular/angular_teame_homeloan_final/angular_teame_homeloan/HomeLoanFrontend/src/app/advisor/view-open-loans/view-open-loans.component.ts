import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { GetAllLoanByadvisorService } from 'src/app/services/get-all-loan-byadvisor.service';
import userLoanApplicationDTO from 'src/app/models/userLoanApplicationDTO';
import { AdvisorService } from 'src/app/services/advisorService';
import AppliedLoanDTO from 'src/app/models/appliedLoanDTO';
@Component({
  selector: 'app-view-open-loans',
  templateUrl: './view-open-loans.component.html',
  styleUrls: ['./view-open-loans.component.css'],
})
export class ViewOpenLoansComponent {
  allopenLoanApplication: userLoanApplicationDTO[];
  appliedLoans: AppliedLoanDTO[];
  isClicked: boolean = false;
  badgeClass: string = '';
  constructor(
    public router: Router,
    public getAllLoanByAdvisorService: GetAllLoanByadvisorService,
    private advisorService: AdvisorService
  ) {}

  displayedColumns: string[];
  dataSource: any;

  @ViewChild(MatPaginator) paginator: MatPaginator;

  ngAfterViewInit() {}

  isHighlight: boolean = true;

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLocaleLowerCase();
  }
  viewDetails(applicationID: number) {
    this.router.navigateByUrl('/advisor/dashboard/view-loan-detail/' + applicationID);
  }

  getEligibilityBadgeColorClass(eligibility: string): string {
    if (eligibility === 'Green') {
      return 'badge rounded-pill text-bg-success';
    } else if (eligibility === 'Red') {
      return 'badge rounded-pill text-bg-danger';
    } else if (eligibility === 'Yellow') {
      return 'badge rounded-pill text-bg-warning';
    }
    return '';
  }

  getStatusBadgeColorClass(status: string) {
    if (status === 'Accepted') {
      return 'badge rounded-pill text-bg-success';
    } else if (status === 'InProgress') {
      return 'badge rounded-pill text-bg-warning';
    } else if (status === 'Rejected') {
      return 'badge rounded-pill text-bg-danger';
    } else if (status === 'Applied') {
      return 'badge rounded-pill text-bg-secondary ';
    }
    return '';
  }
  async ngOnInit() {
    this.displayedColumns = [
      'emailId',
      'loanStartDate',
      'loanDuration',
      'loanAmount',
      'eligibility',
      'view',
      'status',
    ];
    this.appliedLoans = await this.advisorService.getAllLoansApplication();
    console.log('loans', this.appliedLoans);

    this.dataSource = new MatTableDataSource(this.appliedLoans);
    this.dataSource.paginator = this.paginator;
  }
}
