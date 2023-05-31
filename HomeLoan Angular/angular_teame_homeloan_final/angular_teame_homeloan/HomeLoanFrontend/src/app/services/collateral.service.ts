import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApplyCollateralDto } from '../models/apply-collateral-dto';
import { TokenService } from './tokenService';
import { Observable } from 'rxjs';
import { CollateralListDto } from '../models/collateral-list-dto';
@Injectable({
  providedIn: 'root'
})
export class CollateralService {
  private url: string="https://localhost:44382/api/Collateral";

  constructor(
    private httpClient:HttpClient, 
    private tokenService:TokenService
    )
    { }

  postCollateral(applyCollateralDto:ApplyCollateralDto):Observable<string>
  {
    const header: HttpHeaders =new HttpHeaders().set('Authorization','Bearer '+this.tokenService.getItem());
    return this.httpClient.post(this.url,applyCollateralDto,{headers:header,responseType:'text'});
  }

  getAllCollaterals()
  {
    const header: HttpHeaders =new HttpHeaders().set('Authorization','Bearer '+this.tokenService.getItem());
    return this.httpClient.get<CollateralListDto []>('https://localhost:44382/api/Collateral',{headers:header})
  }

  linkCollateralToLoan(loanId:string,collateralId:string)
  {
    const header: HttpHeaders =new HttpHeaders().set('Authorization','Bearer '+this.tokenService.getItem());
    
    const urlLink=this.url+"/LinkCollateralToLoanApplicationActionTask?collaterId="+collateralId+"&applicationId="+loanId;
    return this.httpClient.post(urlLink,{},{headers:header,responseType:'text'});
  }
}
