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
import editcollateralDTO from 'src/app/models/editcollateralDTO';
import { EditCollateralService } from 'src/app/services/edit-collateral.service';

@Component({
  selector: 'app-edit-collateral',
  templateUrl: './edit-collateral.component.html',
  styleUrls: ['./edit-collateral.component.css'],
  animations: [slideInOutAnimation],
  host: { '[@slideInOutAnimation]': '' }
  
})
export class EditCollateralComponent {

 
  constructor(private fb: FormBuilder, public editcollateralService:EditCollateralService,private route: Router, private router: ActivatedRoute) {}
  id:any;
  type:string;
  value:any;
  share:any;
  editCollateralForm:FormGroup;
 
  ngOnInit(): void {
    this.id=this.router.snapshot.paramMap.get('id');
   
    this.type=this.router.snapshot.paramMap.get('type');
   
    this.value=this.router.snapshot.paramMap.get('value');
     
    this.share=this.router.snapshot.paramMap.get('share');
    this.editCollateralForm=this.fb.group(
    {
    id:new FormControl(this.id),
     type:new FormControl(this.type),
      
     value: new FormControl(this.value,[Validators.required,Validators.min(1000),Validators.max(990000000)]),
      
     share: new FormControl(this.share,[Validators.required,Validators.min(1),Validators.max(100)]),
      
      }
     );
    
  }
  
  NavigateToViewCollateral()
  {
    this.route.navigateByUrl('/users/view-collateral');
  }
  SaveCollateralChanges()
  {
    if(this.editCollateralForm.valid)
    {
      this.editcollateralService.editCollateral(this.editCollateralForm.value);
    alert("Value Updated Successfully");
    this.route.navigateByUrl('/users/view-collateral');
    }
    else{
      this.validateAllFields(this.editCollateralForm);
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
  get Id()
  {
    return this.editCollateralForm.get('id');
  }
  get Type()
  {
    return this.editCollateralForm.get('type');
  } get Value()
  {
    return this.editCollateralForm.get('value');
  } get Share()
  {
    return this.editCollateralForm.get('share');
  }
}
