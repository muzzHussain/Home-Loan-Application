import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { NavigationExtras, Router } from '@angular/router';
import locationDTO from 'src/app/models/locationDTO';
import { GetLocationService } from 'src/app/services/get-location.service';
@Component({
  selector: 'app-location',
  templateUrl: './location.component.html',
  styleUrls: ['./location.component.css']
})
export class LocationComponent {
  constructor(
    public router: Router,
    public locationService:GetLocationService,
   
    
  ) {}
  public locationdtolist:locationDTO[]
  displayedColumns: string[];
  dataSource: any;

  @ViewChild(MatPaginator) paginator: MatPaginator;

  ngAfterViewInit() {}

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLocaleLowerCase();
  }
  editDetails(element:any) {
   
    let navigationExtras: NavigationExtras = {
      queryParams: {
      "element": JSON.stringify(element)
      }};
     
      this.router.navigate(["advisor/dashboard/edit-location"], navigationExtras);
    
  }
  async ngOnInit() {
    this.displayedColumns = [
      'countryName',
      'stateName',
      'cityName',
      
      'edit',
    ];
    this.locationdtolist =
      await this.locationService.GetAllLocationDetails();
    
    this.dataSource = new MatTableDataSource(this.locationdtolist);
    this.dataSource.paginator = this.paginator;
  }
}



