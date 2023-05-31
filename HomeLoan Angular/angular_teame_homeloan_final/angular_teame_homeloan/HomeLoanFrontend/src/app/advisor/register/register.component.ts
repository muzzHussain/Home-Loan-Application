import { Component,OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  public advisorRegisterForm = new FormGroup({
    emailId: new FormControl<string>('', [Validators.required, Validators.email]),
      password: new FormControl<string>('', [Validators.required, Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$')])
  })
  public get emailId() {
    return this.advisorRegisterForm.get("emailId");
  }
  public get password() {
    return this.advisorRegisterForm.get("password");
  }
  OnClickSubmit() {
    if (this.advisorRegisterForm.invalid) {
      alert("Invalid Details")
    }
    
  }
  
}
