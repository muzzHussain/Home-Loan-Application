import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TokenService } from './tokenService';
import { Observable } from 'rxjs/internal/Observable';
import { EndpointService } from './endpoint.service';
import AppliedLoanDTO from 'src/app/models/appliedLoanDTO';
import { ApplyLoanDto } from '../models/apply-loan-dto';

@Injectable({
  providedIn: 'root',
})
export class AdvisorService {
  apiURL = 'https://localhost:44382/api/advisor/';
  baseServerUrl = 'https://localhost:44382/api';
  loanApiURL = 'https://localhost:44382/api/Loan';

  private token: any;
  res: any;
  appliedLoanRes: AppliedLoanDTO[];

  constructor(
    private httpClient: HttpClient,
    private tokenService: TokenService,
    private endPoints: EndpointService
  ) {}

  async login(emailId: string, password: string) {
    try {
      const head = new HttpHeaders()
        .set('Content-Type', 'application/json')
        .set('emailId', emailId)
        .set('password', password);

      this.res = await this.httpClient
        .get(`${this.apiURL}`, {
          headers: head,
          responseType: 'text',
        })
        .toPromise();

      const decodeToken = this.decodeToken(this.res);
      const role =
        decodeToken[
          'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
        ];

      this.tokenService.setItem(this.res);
      this.tokenService.setRole(role);

      const isLoggedIn = this.isLoggedIn();
      return isLoggedIn;
    } catch (error) {
      throw error;
    }
  }

  async updatePassword(password: string, newPassword: string) {
    const body = { password, newPassword };

    const head = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Authorization', 'Bearer ' + localStorage.getItem('jwt_token'));
    this.res = await this.httpClient
      .put(this.apiURL, body, { headers: head, responseType: 'text' })
      .toPromise();
    return this.res;
  }

  async changeStatusOfAppliedLoans(id: string, status: string, notes: string) {
    const queryParams = `?Id=${id}&Status=${status}&Notes=${notes}`;
    const head = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Authorization', 'Bearer ' + localStorage.getItem('jwt_token'));
    this.res = await this.httpClient
      .put(this.endPoints.ChangeLoanStatusByAdvisorTask + queryParams, null, {
        headers: head,
        responseType: 'text',
      })
      .toPromise();
    console.log('resp', this.res);

    return this.res;
  }

  isLoggedIn() {
    const token = localStorage.getItem('jwt_token');
    return token !== null && token !== undefined;
  }

  async logout() {
    await this.tokenService.removeItem();
    await this.tokenService.removeRole();
  }

  private decodeToken(token: string) {
    try {
      return JSON.parse(atob(token.split('.')[1]));
    } catch (error) {
      console.log('Error while decoding token.');
      return null;
    }
  }

  getAllCountries(): Observable<any> {
    return this.httpClient.get(
      `${this.baseServerUrl}/CountryStateCity/GetAllCountriesTask`
    );
  }
  getAllStates(countryName: string): Observable<any> {
    return this.httpClient.get(
      `${this.baseServerUrl}/CountryStateCity/GetAllStateTask?countryName=${countryName}`
    );
  }
  getAllCities(stateName: string, countryName: string): Observable<any> {
    return this.httpClient.get(
      `${this.baseServerUrl}/CountryStateCity/GetAllCitiesTask?countryName=${countryName}&stateName=${stateName}`
    );
  }

  async getAllLoansApplication(): Promise<AppliedLoanDTO[]> {
    const head = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Authorization', 'Bearer ' + localStorage.getItem('jwt_token'));

    this.appliedLoanRes = await this.httpClient
      .get<AppliedLoanDTO[]>(
        `${this.endPoints.GetAllLoanApplicationByAdvisorTask}`,
        {
          headers: head,
        }
      )
      .toPromise();

    return this.appliedLoanRes;
  }

  async getAllLoansApplicationDetails(id: string): Promise<AppliedLoanDTO[]> {
    const head = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Authorization', 'Bearer ' + localStorage.getItem('jwt_token'));
    this.appliedLoanRes = await this.httpClient
      .get<AppliedLoanDTO[]>(
        this.endPoints.GetAnAppliedLoanApplicationByAdvisorTask + id,
        { headers: head }
      )
      .toPromise();
    return this.appliedLoanRes;
  }
}
