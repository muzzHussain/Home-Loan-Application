
import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { GetAllCollateralService } from 'src/app/services/get-all-collateral.service';
import CollateralDTO from 'src/app/models/CollateralDTO';
import { DeleteCollateralService } from 'src/app/services/delete-collateral.service';
@Component({
  selector: 'app-view-collateral',
  templateUrl: './view-collateral.component.html',
  styleUrls: ['./view-collateral.component.css']
})
export class ViewCollateralComponent  {
  allCollateralList: CollateralDTO[];
  constructor(
    public router: Router,
    public getallCollateralService: GetAllCollateralService,
    public deleteCollateralservice: DeleteCollateralService
  ) {}

  displayedColumns: string[];
  dataSource: any;
  
    response:boolean;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  ngAfterViewInit() {}

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLocaleLowerCase();
  }

  back(){
    this.router.navigateByUrl("/users/dashboard");
  }
  async deletecollateral(collateralID:any)
  {
    this.response=await this.deleteCollateralservice.DeleteCollateral(collateralID);
    
    
    this.allCollateralList =
      await this.getallCollateralService.GetAllCollateral();
      
      
    this.dataSource = new MatTableDataSource(this.allCollateralList);
    this.dataSource.paginator = this.paginator;
    this.router.navigateByUrl('/users/view-collateral');
    alert(`Collateral with Id ${collateralID} is deleted`);
  }
  editdetails(collateralId: number, type:string, value:number, share:number) {
    
    this.router.navigateByUrl('/users/edit-collateral/'+collateralId+'/'+type+'/'+value+'/'+share);
  }
  async ngOnInit() {
    this.displayedColumns = [
      'id',
      'type',
      'value',
      'share',
      'view',
    ];
    this.allCollateralList =
      await this.getallCollateralService.GetAllCollateral();
      

    this.dataSource = new MatTableDataSource(this.allCollateralList);
    this.dataSource.paginator = this.paginator;
  }
}
