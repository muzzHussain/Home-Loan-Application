import { Component } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ApplyCollateralDto } from 'src/app/models/apply-collateral-dto';
import { CollateralService } from 'src/app/services/collateral.service';
import { UserService } from 'src/app/services/userService';

@Component({
  selector: 'app-add-collateral',
  templateUrl: './add-collateral.component.html',
  styleUrls: ['./add-collateral.component.css']
})
export class AddCollateralComponent {
 collateralForm:FormGroup;

  isSubmitted:boolean;
  errorMsg: string;
  successMsg: string;
  constructor(
    private formBuilder:FormBuilder, 
    private service:CollateralService, 
    private router: Router
    )
  {
    this.isSubmitted=false;
    this.errorMsg="";
    this.successMsg="";
    this.collateralForm=new FormGroup({});
  }
  
  ngOnInit(): void {
    this.collateralForm=this.formBuilder.group(
      {
        type:new FormControl('',[Validators.required]),
        value: new FormControl(null,[Validators.required,Validators.min(1000),Validators.max(990000000)]),
        share: new FormControl(null,[Validators.required,Validators.min(1),Validators.max(100)]),
      }
    );
  }

  back():void{
    this.router.navigateByUrl("/users/dashboard");
  }

   onSubmit():void{
    if(this.collateralForm.valid)
     {
      //console.log(this.collateralForm.value);
      this.isSubmitted=true;
      const collateralDto: ApplyCollateralDto=this.formToDtoConversion();
      this.service.postCollateral(collateralDto).subscribe(
      {
        next: (response)=>{
         // console.log(response);
          this.errorMsg='';
          this.successMsg="Collateral Added Successfully";
          this.isSubmitted=false;
          setTimeout(()=>{
            this.successMsg="";
          }, 2000)
          this.collateralForm.reset();
        },
        error: (err)=>{
          this.isSubmitted=false;
          if(err.status===400)
          {
            this.errorMsg="Please check your request/inputs and try again"
          }
          else if(err.status===401)
          {
            this.errorMsg="Session timeout redirecting to login please wait";
            setTimeout(()=>{
              this.errorMsg="";
              this.router.navigateByUrl('/login');
            }, 2000)

          }
          else if(err.status===500)
          {
            this.errorMsg="Internal Server Error";
          }
          else
          {
            this.errorMsg="Service is down";
          }
         // console.log(err)
        }
      })
    }
    else{
      this.validateAllFields(this.collateralForm);
    }
   }
  

   get type()
   {
     return this.collateralForm.get('type')!;
   }
   get value()
   {
     return this.collateralForm.get('value')!
   }
   get share()
   {
     return this.collateralForm.get('share')!;
   }
 
   
private formToDtoConversion(): ApplyCollateralDto {
    return this.collateralForm.value;
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
}
