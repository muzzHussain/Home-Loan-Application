import { BootstrapOptions, Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { slideInOutAnimation } from 'src/app/animation/slide-in-out.animation';
import { UserService } from 'src/app/services/userService';
import { AdvisorService } from 'src/app/services/advisorService';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  animations: [slideInOutAnimation],
  host: { '[@slideInOutAnimation]': '' },
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  alert = false;
  loginForm!: FormGroup;
  email = 'advisor@advisor.com';
  errs!: any;
  password = 'advisor';
  userLogin: boolean = false;
  advisorLogin: boolean = false;
  successMessage: string = '';
  errorMessage: string = '';
  alertClass: string = '';

  constructor(
    private fb: FormBuilder,
    private route: Router,
    private userService: UserService,
    private advisorService: AdvisorService
  ) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }

  get getEmail() {
    return this.loginForm.get('email');
  }
  get getPassword() {
    return this.loginForm.get('password');
  }
  async onUserLogin() {
    try {
      if (this.loginForm.valid) {
        this.userLogin = true;
        const email = this.loginForm.value.email;
        const password = this.loginForm.value.password;
        await this.userService.login(email, password);

        this.alertClass = 'alert alert-success';
        this.alert = true;
        this.successMessage = 'Login Successful.';

        setTimeout(() => {
          this.route.navigateByUrl('/users/dashboard');
        }, 600);
      } else {
        this.validateAllFields(this.loginForm);
      }
    } catch (error) {
      this.loginForm = this.fb.group({
        email: '',
        password: '',
      });
      this.userLogin = false;
      this.alertClass = 'alert alert-danger';
      this.alert = true;
      this.errs = 'An error occured while login.';
      setTimeout(() => {
        this.alert = false;
      }, 1100);
      this.errs = 'login failed.';
    }
  }

  async onAdvisorLogin() {
    try {
      if (this.loginForm.valid) {
        this.advisorLogin = true;
        const email = this.loginForm.value.email;
        const password = this.loginForm.value.password;
        await this.advisorService.login(email, password);

        this.alertClass = 'alert alert-success';
        this.alert = true;
        this.successMessage = 'Login Successfull.';

        setTimeout(() => {
          // this.route.navigateByUrl('view-all-open-loan');
          this.route.navigateByUrl('advisor/dashboard');
        }, 600);
      } else {
        this.validateAllFields(this.loginForm);
      }
    } catch (error) {
      this.loginForm = this.fb.group({
        email: '',
        password: '',
      });
      this.advisorLogin = false;
      this.alertClass = 'alert alert-danger';
      this.alert = true;
      this.errorMessage = 'An error occured while login.';
      setTimeout(() => {
        this.alert = false;
      }, 1100);
      this.errs = 'Login Failed.';
    }
  }

  private validateAllFields(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach((field) => {
      const control = formGroup.get(field);
      if (control instanceof FormControl) {
        control.markAsTouched({ onlySelf: true });
      } else if (control instanceof FormGroup) {
        this.validateAllFields(control);
      }
    });
  }

  closeAlert() {
    this.alert = false;
  }
}
