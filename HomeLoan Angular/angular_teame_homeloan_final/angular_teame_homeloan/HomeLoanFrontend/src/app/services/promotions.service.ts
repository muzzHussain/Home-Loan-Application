import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Promotion } from '../interfaces/Promotion';
import { TokenService } from './tokenService';

@Injectable({
  providedIn: 'root',
})
export class PromotionsService {
  baseServerUrl = 'https://localhost:44382/api';

  constructor(private httpClient: HttpClient,private tokenService: TokenService) {}
  head = new HttpHeaders({
    'Content-Type': 'application/json',
    Authorization: `Bearer ${this.tokenService.getItem()}`,
  });
  getPromotion(): Observable<Promotion> {
    return this.httpClient.get(
      `${this.baseServerUrl}/Promotions`
    ) as Observable<Promotion>;
  }

  addPromotion(promotion: Promotion): Observable<string> {
    return this.httpClient.post(`${this.baseServerUrl}/Promotions`, promotion, {
      headers: this.head,
      responseType: 'text',
    }) as Observable<string>;
  }
}
