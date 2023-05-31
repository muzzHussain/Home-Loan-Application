import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

import { EndpointService } from './endpoint.service';
import { ModelInitializerService } from './model-initializer.service';
import locationDTO from '../models/locationDTO';
@Injectable({
  providedIn: 'root'
})
export class GetLocationService {

  constructor(public httpClient: HttpClient, public router: Router, public endpoints: EndpointService, public modelInitializer: ModelInitializerService) { }
  
  public res:locationDTO[]
  public async GetAllLocationDetails(): Promise<locationDTO[]> {
    
    const head = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization':`Bearer ${localStorage.getItem("jwt_token")}`
      
    })
    
    
    this.res = await this.httpClient.get<locationDTO[]>(`${this.endpoints.getLocationService}`,  { headers: head }).toPromise();
    
    return this.res;
  }
}
