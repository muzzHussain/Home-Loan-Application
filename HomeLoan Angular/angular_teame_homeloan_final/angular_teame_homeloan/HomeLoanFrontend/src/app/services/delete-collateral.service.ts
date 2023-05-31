import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

import { EndpointService } from './endpoint.service';
import { ModelInitializerService } from './model-initializer.service';
@Injectable({
  providedIn: 'root'
})
export class DeleteCollateralService {

  constructor(public httpClient: HttpClient, public router: Router, public endpoints: EndpointService, public modelInitializer: ModelInitializerService) { }
  
  public res: boolean ;
  public async DeleteCollateral(collateralId:any): Promise<boolean> {
    
    const head = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization':`Bearer ${localStorage.getItem("jwt_token")}`
      
    })
    
    
    this.res = await this.httpClient.delete<boolean>(`${this.endpoints.DeleteCollateral+collateralId}`,  { headers: head }).toPromise();
    
    return this.res;
  }
}
