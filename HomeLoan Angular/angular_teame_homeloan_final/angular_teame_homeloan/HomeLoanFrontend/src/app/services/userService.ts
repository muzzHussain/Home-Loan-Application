import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TokenService } from './tokenService';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  apiURL = 'https://localhost:44382/api/user/';
  private token: any;
  res!: any;
  link = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';
  constructor(
    private httpClient: HttpClient,
    private tokenService: TokenService
  ) {}

  async login(emailId: string, password: string) {
    try {
      const head = new HttpHeaders()
        .set('Content-Type', 'application/json')
        .set('emailId', emailId)
        .set('password', password);

      const resp: any = await this.httpClient
        .get(`${this.apiURL}`, { headers: head, responseType: 'text' })
        .toPromise();

      const decodeToken = this.decodeToken(resp);
      const role =
        decodeToken[
          'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
        ];

      this.tokenService.setItem(resp);
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

  isLoggedIn(): boolean {
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
      console.log('Error while decoding', error);
      return null;
    }
  }

  async register(userData:any){
    
  }

  registerUser(user: any) {
    return this.httpClient.post(this.apiURL, user);
  }
}
