import { Injectable } from '@angular/core';
import { AdvisorService } from './advisorService';
import { UserService } from './userService';
import { CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private advisorService: AdvisorService,
    private userService: UserService,
    private router: Router
  ) {}

  canActivate() {
    if (this.advisorService.isLoggedIn()) {
      return true;
    } else {
      this.router.navigate(['/login']);
      return false;
    }
  }
}
