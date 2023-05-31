import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { slideInOutAnimation } from 'src/app/animation/slide-in-out.animation';
import { EditLocationService } from 'src/app/services/edit-location.service';

@Component({
  selector: 'app-edit-location',
  templateUrl: './edit-location.component.html',
  styleUrls: ['./edit-location.component.css']
})
export class EditLocationComponent {
  
  constructor(private fb: FormBuilder, public editLocationService:EditLocationService,private route: Router, private router: ActivatedRoute) {}
  countryName:string;
  countryCode:string;
  stateName:string;
  stateCode:string;
  cityName:string;
  cityCode:string;
  countryId:any;
  stateId:any;
  cityId:any;
  editLocationForm:FormGroup;
  elementdata:any;
  formdata:any;
  ngOnInit(): void {
    this.router.queryParams.subscribe(params => {
      this.elementdata = JSON.parse(params["element"]);
      });
      console.log(this.elementdata);
      this.countryName=this.elementdata.countryName;
      this.countryCode=this.elementdata.countryCode;
      this.stateName=this.elementdata.stateName;
      this.stateCode=this.elementdata.stateCode;
      this.cityCode=this.elementdata.cityCode;
      this.cityName=this.elementdata.cityName;
      this.countryId=this.elementdata.countryId;
      this.stateId=this.elementdata.stateId;
      this.cityId=this.elementdata.cityId;
    this.editLocationForm=this.fb.group(
    {
    countryName:new FormControl(this.countryName,[Validators.required,Validators.minLength(2),Validators.maxLength(20)]),
     countryCode:new FormControl(this.countryCode,[Validators.required,Validators.minLength(2),Validators.maxLength(3)]),
     stateName:new FormControl(this.stateName,[Validators.required,Validators.minLength(2),Validators.maxLength(20)]),
     stateCode:new FormControl(this.stateCode,[Validators.required,Validators.minLength(2),Validators.maxLength(3)]),
     cityName:new FormControl(this.cityName,[Validators.required,Validators.minLength(2),Validators.maxLength(20)]),
     cityCode:new FormControl(this.cityCode,[Validators.required,Validators.minLength(2),Validators.maxLength(3)]),
    
      }
     );
    
  }
  
  NavigateToViewLocation()
  {
    this.route.navigateByUrl('advisor/dashboard/view-location');
  }
 async  SaveCollateralChanges()
  {
    if(this.editLocationForm.valid)
    {
      this.formdata= this.editLocationForm.value;
      
      await this.editLocationService.EditCountry(this.countryId,this.formdata.countryCode,this.formdata.countryName);
      await this.editLocationService.EditState(this.stateId,this.formdata.stateCode,this.formdata.stateName,this.formdata.countryCode);
      await this.editLocationService.EditCity(this.cityId,this.formdata.cityCode,this.formdata.cityName,this.formdata.stateCode);

      alert("Value Updated Successfully");
      this.route.navigateByUrl('advisor/dashboard/view-location');
    }
  else{
    this.validateAllFields(this.editLocationForm);
    }
    
    // this.editcollateralService.editCollateral(this.editLocationForm.value);
  
    // this.route.navigateByUrl('view-collateral');
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
  get CountryName()
  {
    return this.editLocationForm.get('countryName');
  }
  get CountryCode()
  {
    return this.editLocationForm.get('countryCode');
  }
  get StateName()
  {
    return this.editLocationForm.get('stateName');
  }
  get StateCode()
  {
    return this.editLocationForm.get('stateCode');
  }
  get CityName()
  {
    return this.editLocationForm.get('cityName');
  }
  get CityCode()
  {
    return this.editLocationForm.get('cityCode');
  }
}
