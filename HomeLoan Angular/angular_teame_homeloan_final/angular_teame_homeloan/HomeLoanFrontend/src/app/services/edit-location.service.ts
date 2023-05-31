import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

import { EndpointService } from './endpoint.service';
import { ModelInitializerService } from './model-initializer.service';
import countryDTO from '../models/countryDTO';
import stateDTO from '../models/stateDTO';
import cityDTO from '../models/cityDTO';

@Injectable({
  providedIn: 'root'
})
export class EditLocationService {

  constructor(public httpClient: HttpClient, public router: Router, public endpoints: EndpointService, public modelInitializer: ModelInitializerService) { }
  
  public res:boolean
  public async EditCountry(id:any,code:string,name:string): Promise<boolean> {
    
    const head = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization':`Bearer ${localStorage.getItem("jwt_token")}`
      
    })
    const countryDtoObj = new countryDTO(id,code,name);
    this.res = await this.httpClient.put<boolean>(`${this.endpoints.editCountryService}`,countryDtoObj,{ headers: head }).toPromise();
    return this.res;
  }

  public async EditState(id:any,code:string,name:string,countryCode:string): Promise<boolean> {
    
    const head = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization':`Bearer ${localStorage.getItem("jwt_token")}`
      
    })
    const statedtoObj = new stateDTO(id,code,name,countryCode);
    this.res = await this.httpClient.put<boolean>(`${this.endpoints.editStateService}`,statedtoObj,{ headers: head }).toPromise();
    return this.res;
  }


  public async EditCity(id:any,code:string,name:string,statecode:string): Promise<boolean> {
    
    const head = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization':`Bearer ${localStorage.getItem("jwt_token")}`
      
    })
    const citydtoObj = new cityDTO(id,code,name,statecode);
    this.res = await this.httpClient.put<boolean>(`${this.endpoints.editCityService}`,citydtoObj,{ headers: head }).toPromise();
    return this.res;
  }
}
