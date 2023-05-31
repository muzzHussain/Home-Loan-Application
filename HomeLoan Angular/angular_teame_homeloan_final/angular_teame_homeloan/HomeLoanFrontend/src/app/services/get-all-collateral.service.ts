import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import CollateralDTO from '../models/CollateralDTO';
import { EndpointService } from './endpoint.service';
import { ModelInitializerService } from './model-initializer.service';
@Injectable({
  providedIn: 'root'
})
export class GetAllCollateralService {
  constructor(public httpClient: HttpClient, public router: Router, public endpoints: EndpointService, public modelInitializer: ModelInitializerService) { }
  
  public res: CollateralDTO[] ;
  public async GetAllCollateral(): Promise<CollateralDTO[]> {
    
    const head = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization':`Bearer ${localStorage.getItem("jwt_token")}`
      
    })
    
    
    this.res = await this.httpClient.get<CollateralDTO[]>(`${this.endpoints.GetAllCollateral}`,  { headers: head }).toPromise();
    
    return this.res;
  }

}
