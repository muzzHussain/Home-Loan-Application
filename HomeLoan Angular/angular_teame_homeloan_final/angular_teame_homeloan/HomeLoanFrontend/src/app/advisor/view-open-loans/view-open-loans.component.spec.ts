import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewOpenLoansComponent } from './view-open-loans.component';

describe('ViewOpenLoansComponent', () => {
  let component: ViewOpenLoansComponent;
  let fixture: ComponentFixture<ViewOpenLoansComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ViewOpenLoansComponent]
    });
    fixture = TestBed.createComponent(ViewOpenLoansComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
