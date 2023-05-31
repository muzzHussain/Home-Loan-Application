import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

import { EndpointService } from './endpoint.service';
import { ModelInitializerService } from './model-initializer.service';
import editcollateralDTO from '../models/editcollateralDTO';
@Injectable({
  providedIn: 'root'
})
export class EditCollateralService {

  constructor(public httpClient: HttpClient, public router: Router, public endpoints: EndpointService, public modelInitializer: ModelInitializerService) { }
  public req:editcollateralDTO;
  public res: boolean ;
  
  public async editCollateral(data:any): Promise<boolean> {
    
    console.log(data);
    const head = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization':`Bearer ${localStorage.getItem("jwt_token")}`
      
    })

    const editcollateralobj = new editcollateralDTO(data.id,Number(data.value),Number(data.share));
    
    
    
    this.res = await this.httpClient.put<boolean>(`${this.endpoints.editCollateralService}`,editcollateralobj,{ headers: head }).toPromise();
    
    return this.res;
  }
}
