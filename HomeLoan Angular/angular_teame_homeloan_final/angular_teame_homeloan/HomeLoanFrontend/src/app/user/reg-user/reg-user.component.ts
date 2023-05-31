import { Component, OnInit, VERSION } from '@angular/core';
import {
  FormControl,
  FormGroup,
  Validators,
  FormBuilder,
} from '@angular/forms';
import { Router } from '@angular/router';
import { slideInOutAnimation } from 'src/app/animation/slide-in-out.animation';
import { AdvisorService } from 'src/app/services/advisorService';
import { UserService } from 'src/app/services/userService';

@Component({
  selector: 'app-reg-user',
  templateUrl: './reg-user.component.html',
  // animations: [slideInOutAnimation],
  // host: { '[@slideInOutAnimation]': '' },
  styleUrls: ['./reg-user.component.css'],
})
export class RegUserComponent implements OnInit {
  alertt: boolean = false;
  alertClass: string = '';
  successMessage: string = '';
  errorMessage: string = '';
  constructor(
    private fb: FormBuilder,
    private _router: Router,
    private _userService: UserService,
    private _advisorService: AdvisorService
  ) {}

  countryList: { code: string; name: string }[] = [];
  stateList: { code: string; name: string; countryCode: string }[] = [];
  cityList: { code: string; name: string; stateCode: string }[] = [];
  alert = false;
  errs!: any;
  registerForm!: FormGroup;
  emailAlreadyExist: boolean = false;

  ngOnInit(): void {
    this.registerForm = this.fb.group({
      emailId: new FormControl('', [Validators.required, Validators.email]),
      password: ['', Validators.required],
      mobileNumber: new FormControl('', [
        Validators.required,
        Validators.pattern('[0-9]{10}'),
      ]),
      country: ['', Validators.required],
      state: ['', Validators.required],
      city: ['', Validators.required],
    });

    this._advisorService
      .getAllCountries()
      .subscribe((data) => (this.countryList = data));
    this.registerForm.controls['country'].valueChanges.subscribe((e) =>
      this.onCountryChange(e)
    );
    this.registerForm.controls['state'].valueChanges.subscribe((e) =>
      this.onStateChange(e)
    );

    const countryValue = this.registerForm.get('country');
    if (countryValue)
      countryValue.valueChanges.subscribe((country) => {
        this.onCountryChange(country);
      });
    const stateValue = this.registerForm.get('state');
    if (stateValue)
      stateValue.valueChanges.subscribe((state) => {
        this.onStateChange(state);
      });
  }

  onCountryChange(country: string) {
    this.stateList = [];
    this.cityList = [];
    this.registerForm.patchValue({ state: '', city: '' });

    this._advisorService
      .getAllStates(country)
      .subscribe((data) => (this.stateList = data));
  }

  onStateChange(state: string) {
    const countryName = this.registerForm.controls['country'].value;
    if (countryName !== '') {
      this._advisorService
        .getAllCities(state, countryName)
        .subscribe((data) => (this.cityList = data));
    }
  }

  get getEmail() {
    return this.registerForm.get('emailId');
  }
  get getPassword() {
    return this.registerForm.get('password');
  }
  get getMobile() {
    return this.registerForm.get('mobileNumber');
  }
  get getCountry() {
    return this.registerForm.get('country');
  }
  get getState() {
    return this.registerForm.get('state');
  }
  get getCity() {
    return this.registerForm.get('city');
  }

  closeAlert() {
    this.alert = false;
    this.registerForm.reset({});
  }

  async registerSubmitted() {
    const user = this.registerForm.value;

    // Finding the corresponding country code based on the selected country
    user.countryCode = this.countryList.find(
      ({ name }) => name === user.country
    )?.code;

    // Finding the corresponding state code based on the selected state and country
    const selectedCountry = this.countryList.find(
      ({ name }) => name === user.country
    );
    user.stateCode = this.stateList.find(
      ({ name, countryCode }) =>
        name === user.state && countryCode === selectedCountry.code
    )?.code;

    // Finding the corresponding city code based on the selected city, state, and country
    const selectedState = this.stateList.find(
      ({ name, code, countryCode }) =>
        name === user.state &&
        code === user.stateCode &&
        countryCode === selectedCountry.code
    );
    user.cityCode = this.cityList.find(
      ({ name, stateCode }) =>
        name === user.city && stateCode === selectedState.code
    )?.code;

    const response = await this._userService.registerUser(user).subscribe({
      next: (resp) => {
        if (resp) {
          this.alertt = true;
          this.alertClass = 'alert alert-success';
          this.successMessage = 'User Registered';

          setTimeout(() => {
            this._router.navigate(['login']);
          }, 1500);
        }
      },
      error: (err) => {
        this.alertt = true;
        this.alertClass = 'alert alert-danger';
        this.errorMessage = 'User already resgistered';
        this.registerForm = this.fb.group({
          emailId: new FormControl('', [Validators.required, Validators.email]),
          password: ['', Validators.required],
          mobileNumber: new FormControl('', [
            Validators.required,
            Validators.pattern('[0-9]{10}'),
          ]),
          country: ['', Validators.required],
          state: ['', Validators.required],
          city: ['', Validators.required],
        });
      },
    });
  }
}
