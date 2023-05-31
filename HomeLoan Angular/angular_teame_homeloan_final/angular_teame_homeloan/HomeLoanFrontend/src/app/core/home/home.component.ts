import { Component } from '@angular/core';
import { Promotion } from 'src/app/interfaces/Promotion';
import { PromotionsService } from 'src/app/services/promotions.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  promotion: Promotion ={startDate: "",
    endDate: "",
    type: "",
    message: "",}
  constructor(private _promotionsService: PromotionsService) {}

  ngOnInit() {
    this._promotionsService
      .getPromotion()
      .subscribe((e) => (this.promotion = e));
  }
}
