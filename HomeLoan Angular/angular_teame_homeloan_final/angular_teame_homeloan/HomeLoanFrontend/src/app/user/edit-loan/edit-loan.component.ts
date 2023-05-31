import { Component } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators, ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { Router } from '@angular/router';
import { EditLoanDto } from 'src/app/models/edit-loan-dto';
import userLoanApplicationDTO from 'src/app/models/userLoanApplicationDTO';
import { LoanApiService } from 'src/app/services/loan-api.service';

@Component({
  selector: 'app-edit-loan',
  templateUrl: './edit-loan.component.html',
  styleUrls: ['./edit-loan.component.css']
})
export class EditLoanComponent {

  loanApplicationForm:FormGroup;
  loanData: userLoanApplicationDTO;
  isSubmitted:boolean;
  errorMsg: string;
  constructor(private formBuilder:FormBuilder, private loanApi:LoanApiService,private router:Router){
    this.loanData=this.router.getCurrentNavigation()?.extras.state['data'];

    console.log(this.loanData);
    this.isSubmitted=false;
    this.errorMsg="";
    this.loanApplicationForm=new FormGroup({});
  }
  
  ngOnInit(): void {

    
      this.loanApplicationForm=this.formBuilder.group(
      {
        address:new FormControl(this.loanData.address,[Validators.required,Validators.minLength(5),Validators.maxLength(100)]),
        size: new FormControl(this.loanData.size,[Validators.required,Validators.min(25),Validators.max(1000)]),
        cost: new FormControl(this.loanData.cost,[Validators.required,Validators.min(100000),Validators.max(900000000),this.costValidator()]),
        registrationCost: new FormControl(this.loanData.registrationCost,[Validators.required,Validators.min(100000),Validators.max(10000000)]),
        monthlyFamilyIncome: new FormControl(this.loanData.monthlyFamilyIncome,[Validators.required,Validators.min(1000),Validators.max(10000000)]),
        otherIncome: new FormControl(this.loanData.otherIncome,[Validators.required,Validators.min(1000),Validators.max(10000000)]),
        loanAmount: new FormControl(this.loanData.loanAmount,[Validators.required,Validators.min(10000),Validators.max(990000000)]),
        loanDuration: new FormControl(this.loanData.loanDuration,[Validators.required,Validators.min(12),Validators.max(240)]),
      }
    );
  }


   onSubmit():void{
    if(this.loanApplicationForm.valid)
    {
      
      const editLoanDto=this.formToDtoConversion();
      this.loanApi.patchLoan(editLoanDto).subscribe( {next: (res)=>{
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
        this.errorMsg="Session time out Redirecting to login wait.."
        setTimeout(()=>{
          this.isSubmitted=false;
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
    this.router.navigateByUrl("/users/dashboard");
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
  const editLoanDto:EditLoanDto=new EditLoanDto();
  editLoanDto.id=this.loanData.id;
  editLoanDto.cost=this.cost.value;
  editLoanDto.address=this.address.value;
  editLoanDto.loanAmount=this.loanAmount.value;
  editLoanDto.loanDuration=this.loanDuration.value;
  editLoanDto.monthlyFamilyIncome=this.monthlyFamilyIncome.value;
  editLoanDto.otherIncome=this.otherIncome.value;
  editLoanDto.registrationCost=this.registrationCost.value;
  editLoanDto.size=this.size.value;
  editLoanDto.loanStartDate=new Date();
  return editLoanDto;
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
