import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewCollateralComponent } from './view-collateral.component';

describe('ViewCollateralComponent', () => {
  let component: ViewCollateralComponent;
  let fixture: ComponentFixture<ViewCollateralComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ViewCollateralComponent]
    });
    fixture = TestBed.createComponent(ViewCollateralComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
