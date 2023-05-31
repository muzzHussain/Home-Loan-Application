import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  private tokenKey = 'jwt_token';
  private roleKey = 'role';

  constructor() {}

  getItem() {
    return localStorage.getItem(this.tokenKey);
  }

  setItem(token: string): void {
    return localStorage.setItem(this.tokenKey, token);
  }

  removeItem(): void {
    return localStorage.removeItem(this.tokenKey);
  }

  getRole() {
    return localStorage.getItem(this.roleKey);
  }

  setRole(role: string) {
    return localStorage.setItem(this.roleKey, role);
  }

  removeRole() {
    return localStorage.removeItem(this.roleKey);
  }
}
