import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import userLoanApplicationDTO from '../models/userLoanApplicationDTO';
import { EndpointService } from './endpoint.service';
import { ModelInitializerService } from './model-initializer.service';

@Injectable({
  providedIn: 'root'
})
export class ViewLoanDetailService {
  constructor(public httpClient: HttpClient, public router: Router, public endpoints: EndpointService, public modelInitializer: ModelInitializerService) { }
  
  public res: userLoanApplicationDTO;
  public async GetLoanDetails(applicationId:any):Promise<userLoanApplicationDTO> {
    
    const head = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization':`Bearer ${localStorage.getItem("jwt_token")}`
      
    })
    
    
    this.res = await this.httpClient.get<userLoanApplicationDTO>(this.endpoints.FetchAnLoanApplicationByIdByAdvisor+applicationId, { headers: head }).toPromise();
    
   
    return this.res;
  }
 
}
