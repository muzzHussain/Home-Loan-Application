import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApplyLoanDto } from '../models/apply-loan-dto';
import { TokenService } from './tokenService';
import userLoanApplicationDTO from '../models/userLoanApplicationDTO';
import { EditLoanDto } from '../models/edit-loan-dto';

@Injectable({
  providedIn: 'root'
})
export class LoanApiService {


  constructor(private httpClient : HttpClient, private tokenService:TokenService) { }

  postLoan(applyLoanDto:ApplyLoanDto)
  {

    return this.httpClient.post('https://localhost:44382/api/Loan',applyLoanDto,{headers: new HttpHeaders({
        'Authorization': 'Bearer '+this.tokenService.getItem(),
      }),
      responseType: 'text'
});
  }

  patchLoan(editLoanDto: EditLoanDto) {
    const header=new HttpHeaders({
      'Authorization': 'Bearer '+this.tokenService.getItem()
    })
    return this.httpClient.patch('https://localhost:44382/api/Loan',editLoanDto,{headers:header,responseType:'text'});
  }
 
  getAllLoans() 
  {
    const header=new HttpHeaders({
      'Authorization': 'Bearer '+this.tokenService.getItem()
    })
    return this.httpClient.get<userLoanApplicationDTO []>('https://localhost:44382/api/Loan',{headers: header})
  }

  applyLoan(loanId:string){

    const urlLink:string= "https://localhost:44382/api/Loan/ChangeApplicationStatusFromCreatedToAppliedByUserTask?applicationId="+loanId;

    const header=new HttpHeaders({
      'Authorization': 'Bearer '+this.tokenService.getItem()
    })
    return this.httpClient.patch(urlLink,{},{headers: header,responseType:'text'})

  }
}
