export default interface userLoanApplicationDTO {
  id: any;
  emailId: string;
  address: string;
  size: number;
  cost: number;
  registrationCost: number;
  monthlyFamilyIncome: number;
  otherIncome: number;
  loanAmount: number;
  loanDuration: number;
  loanStartDate: string;
  status: string;
  notes: string;
}
