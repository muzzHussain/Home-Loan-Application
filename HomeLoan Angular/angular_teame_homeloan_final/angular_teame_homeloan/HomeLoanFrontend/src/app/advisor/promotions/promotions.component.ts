import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { PromotionsService } from 'src/app/services/promotions.service';

@Component({
  selector: 'app-promotions',
  templateUrl: './promotions.component.html',
  styleUrls: ['./promotions.component.css'],
})
export class PromotionsComponent implements OnInit {
  alertt: boolean = false;
  alertClass: string = '';
  successMessage: string = '';
  errorMessage: string = '';
  dateFormat: string = 'yyyy-MM-dd';
  language: string = 'en';
  minDate: any = '';
  nextDate: any = new Date();
  constructor(
    private fb: FormBuilder,
    private _router: Router,
    private _promotion: PromotionsService
  ) {}

  alert = false;
  errs!: any;
  AddPromotion!: FormGroup;
  emailAlreadyExist: boolean = false;

  ngOnInit(): void {
    this.AddPromotion = this.fb.group({
      // startDate: new FormControl('', [Validators.required]),
      // endDate: new FormControl('', [Validators.required]),
      startDate: new FormControl(this.formatFormDate(new Date())),
      endDate: new FormControl(this.formatFormNextDate(new Date())),
      type: ['', Validators.required],
      message: ['', Validators.required],
    });
  }

  formatFormDate(date: Date) {
    this.minDate = formatDate(date, this.dateFormat, this.language);
    return this.minDate;
  }

  formatFormNextDate(date: Date) {
    this.nextDate = date;
    this.nextDate.setDate(this.nextDate.getDate() + 1);
    this.nextDate = formatDate(this.nextDate, this.dateFormat, this.language);
    return this.nextDate;
  }

  get StartDate() {
    return this.AddPromotion.get('StartDate');
  }

  get endDate() {
    return this.AddPromotion.get('StartDate');
  }

  get PromotionType() {
    return this.AddPromotion.get('PromotionType');
  }

  get message() {
    return this.AddPromotion.get('message');
  }

  closeAlert() {
    this.alert = false;
    this.AddPromotion.reset({});
  }

  async AddingPromotion() {
    this._promotion.addPromotion(this.AddPromotion.value).subscribe((e) => {
      setTimeout(() => {
        this._router.navigate(['advisor/dashboard']);
      });
    });
  }
}
