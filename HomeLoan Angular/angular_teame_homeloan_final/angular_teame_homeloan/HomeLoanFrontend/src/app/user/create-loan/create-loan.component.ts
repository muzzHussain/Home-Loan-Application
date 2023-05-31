import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { ApplyLoanDto } from 'src/app/models/apply-loan-dto';
import { LoanApiService } from 'src/app/services/loan-api.service';
import { UserService } from 'src/app/services/userService';

@Component({
  selector: 'app-create-loan',
  templateUrl: './create-loan.component.html',
  styleUrls: ['./create-loan.component.css']
})
export class CreateLoanComponent {
  loanApplicationForm:FormGroup;

  isSubmitted:boolean;
  errorMsg: string;
  constructor(private formBuilder:FormBuilder, private loanApi:LoanApiService,private router:Router){
    this.isSubmitted=false;
    this.errorMsg="";
    this.loanApplicationForm=new FormGroup({});
  }
  
  ngOnInit(): void {
    this.loanApplicationForm=this.formBuilder.group(
      {
        address:new FormControl('',[Validators.required,Validators.minLength(5),Validators.maxLength(100)]),
        size: new FormControl(null,[Validators.required,Validators.min(25),Validators.max(1000)]),
        cost: new FormControl(null,[Validators.required,Validators.min(100000),Validators.max(900000000),this.costValidator()]),
        registrationCost: new FormControl(null,[Validators.required,Validators.min(100000),Validators.max(10000000)]),
        monthlyFamilyIncome: new FormControl(null,[Validators.required,Validators.min(1000),Validators.max(10000000)]),
        otherIncome: new FormControl(null,[Validators.required,Validators.min(1000),Validators.max(10000000)]),
        loanAmount: new FormControl(null,[Validators.required,Validators.min(10000),Validators.max(990000000)]),
        loanDuration: new FormControl(null,[Validators.required,Validators.min(12),Validators.max(240)]),
      }
    );
  }


   onSubmit():void{
    if(this.loanApplicationForm.valid)
    {
      
      let applyLoanDto;
      applyLoanDto=this.formToDtoConversion();
      this.loanApi.postLoan(applyLoanDto).subscribe( {next: (res)=>{
        this.isSubmitted=true;
        setTimeout(()=>{
          this.isSubmitted=false;
          this.errorMsg='';
          this.router.navigateByUrl("/users/dashboard");
        },2000)  
        this.loanApplicationForm.reset();
    },error: err=>{
      console.log(err);
      if(err.status==400)
      {
        this.errorMsg=err.error;
      }
      else if(err.status==401)
      {
        this.errorMsg="Session time out please login again"
        setTimeout(()=>{
          this.errorMsg='';
          
          this.router.navigateByUrl("/login");
        },2000) 
      }
      else if(err.status==500)
      { 
        this.errorMsg="Internal Server Error"
      }
      else
      {
        this.errorMsg="Service Down"
      }
    }})

  }
  else{
    this.validateAllFields(this.loanApplicationForm);
  }
   }

  back():void{
    this.router.navigateByUrl("users/dashboard");
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

private formToDtoConversion()
{
  let applyLoanDto=new ApplyLoanDto();
  applyLoanDto.address=this.address.value;
  applyLoanDto.cost=this.cost.value;
  applyLoanDto.loanAmount=this.loanAmount.value;
  applyLoanDto.loanDuration=this.loanDuration.value;
  applyLoanDto.loanStartDate=new Date();
  applyLoanDto.monthlyFamilyIncome=this.monthlyFamilyIncome.value;
  applyLoanDto.otherIncome=this.otherIncome.value;
  applyLoanDto.registrationCost=this.registrationCost.value;
  applyLoanDto.size=this.size.value;
  this.loanApplicationForm.value.loanStartDate=new Date();
  return applyLoanDto;
}

 private costValidator(): ValidatorFn {
    return (control :AbstractControl): ValidationErrors | null =>{
      const cost=control.value;
      if(cost<100000||cost>900000000||cost%1000==0)
        return null;
      else
      {
        return {'invalidCost': true};
      }
  }
  }

  get address()
  {
    return this.loanApplicationForm.get('address')!;
  }
  get size()
  {
    return this.loanApplicationForm.get('size')!
  }
  get cost()
  {
    return this.loanApplicationForm.get('cost')!;
  }
  get registrationCost()
  {
    return this.loanApplicationForm.get('registrationCost')!;
  }
  get monthlyFamilyIncome()
  {
    return this.loanApplicationForm.get('monthlyFamilyIncome')!;
  }
  get otherIncome()
  {
    return this.loanApplicationForm.get('otherIncome')!;
  }
  get loanAmount()
  {
    return this.loanApplicationForm.get('loanAmount')!;
  }
  get loanDuration()
  {
    return this.loanApplicationForm.get('loanDuration')!;
  }
  
 
}
