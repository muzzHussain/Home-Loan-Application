import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegUserComponent } from './reg-user.component';

describe('RegUserComponent', () => {
  let component: RegUserComponent;
  let fixture: ComponentFixture<RegUserComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RegUserComponent]
    });
    fixture = TestBed.createComponent(RegUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
