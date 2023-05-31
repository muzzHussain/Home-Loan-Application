import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/userService';
import { Router } from '@angular/router';
import { TokenService } from 'src/app/services/tokenService';
import { AdvisorService } from 'src/app/services/advisorService';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  isLoggedIn = false;

  constructor(
    private router: Router,
    private userService: UserService,
    private tokenService: TokenService,
    private advisorService: AdvisorService
  ) {}
  ngOnInit(): void {}

  onLogout() {
    if (this.tokenService.getRole() === 'User') {
      

      this.userService.logout();
      this.router.navigateByUrl('');
    } else {

      this.advisorService.logout();
      this.router.navigateByUrl('');
    }
  }

  get isAuthenticated(): boolean {
    return this.userService.isLoggedIn();
  }
}
