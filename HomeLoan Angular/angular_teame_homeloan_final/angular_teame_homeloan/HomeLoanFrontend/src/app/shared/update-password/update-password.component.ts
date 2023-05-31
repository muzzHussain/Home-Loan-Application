import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  FormControl,
  FormBuilder,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { slideInOutAnimation } from 'src/app/animation/slide-in-out.animation';
import { AdvisorService } from 'src/app/services/advisorService';
import { TokenService } from 'src/app/services/tokenService';
import { UserService } from 'src/app/services/userService';

@Component({
  selector: 'app-update-password',
  templateUrl: './update-password.component.html',
  animations: [slideInOutAnimation],
  host: { '[@slideInOutAnimation]': '' },
  styleUrls: ['./update-password.component.css'],
})
export class UpdatePasswordComponent implements OnInit {
  alert: boolean = false;
  successMessage: string = '';
  errorMessage: string = '';
  errs: string = '';
  res: any = '';
  updatePasswordForm!: FormGroup;
  update = false;
  alertClass: string = '';

  constructor(
    private fb: FormBuilder,
    private advisorService: AdvisorService,
    private route: Router,
    private tokenService: TokenService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.updatePasswordForm = this.fb.group({
      currentPwd: ['', Validators.required],
      newPwd: ['', Validators.required],
      cfNewPwd: ['', Validators.required],
    });
  }

  get getCurrentPassword() {
    return this.updatePasswordForm.get('currentPwd');
  }
  get getConfirmPassword() {
    return this.updatePasswordForm.get('cfNewPwd');
  }
  get getNewPassword() {
    return this.updatePasswordForm.get('newPwd');
  }

  async onUpdate() {
    try {
      if (this.updatePasswordForm.valid) {
        const password = this.updatePasswordForm.value.currentPwd;
        const newPassword = this.updatePasswordForm.value.newPwd;
        if (this.tokenService.getRole() === 'Advisor') {
          console.log('Advisor');

          this.res = await this.advisorService.updatePassword(
            password,
            newPassword
          );
        } else {
          console.log('user');

          this.res = await this.userService.updatePassword(
            password,
            newPassword
          );
        }

        if (this.res.toString() === 'true') {
          if (this.tokenService.getRole() === 'Advisor') {
            this.update = true;
            this.successMessage = 'Password Updated Successfully';
            this.alertClass = 'alert alert-success';
            setTimeout(() => {
              this.route.navigateByUrl('view-all-open-loan');
            }, 1000);
          } else {
            this.update = true;
            this.successMessage = 'Password Updated Successfully';
            this.alertClass = 'alert alert-success';
            setTimeout(() => {
              this.route.navigateByUrl('users/dashboard');
            }, 1000);
          }
        } else {
          this.errorMessage = 'Error updating password.';
          this.alertClass = 'alert alert-danger';
        }
        this.alert = true;
      } else {
        this.errorMessage = 'Please enter all the required fields.';
        this.alert = true;
        this.validateAllFields(this.updatePasswordForm);
      }
    } catch (error) {
      this.errorMessage = 'An error occured while updating the password.';
      this.alertClass = 'alert alert-danger';
      this.alert = true;
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

  back() {
    this.route.navigateByUrl('view-all-open-loan');
  }

  closeAlert() {
    this.alert = false;
  }
}
