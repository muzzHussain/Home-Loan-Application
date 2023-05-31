import { Injectable } from '@angular/core';
import userLoanApplicationDTO from '../models/userLoanApplicationDTO';
import response from '../models/response';
import CollateralDTO from '../models/CollateralDTO';
import editcollateralDTO from '../models/editcollateralDTO';
import locationDTO from '../models/locationDTO';
import countryDTO from '../models/countryDTO';
import stateDTO from '../models/stateDTO';
import cityDTO from '../models/cityDTO';

@Injectable({
  providedIn: 'root',
})
export class ModelInitializerService {
  public citydto: cityDTO = {
    id: '',
    name: '',
    code: '',
    stateCode: '',
  };
  public statedto: stateDTO = {
    id: '',
    name: '',
    code: '',
    countryCode: '',
  };
  public countrydto: countryDTO = {
    id: '',
    name: '',
    code: '',
  };

  public location: locationDTO = {
    countryName: '',
    stateName: '',
    cityName: '',
    countryCode: '',
    stateCode: '',
    cityCode: '',
    countryId: '',
    stateId: '',
    cityId: '',
  };

  public userLoanApplication: userLoanApplicationDTO = {
    id: '',
    emailId: '',
    address: '',
    size: 0,
    cost: 0,
    registrationCost: 0,
    monthlyFamilyIncome: 0,
    otherIncome: 0,
    loanAmount: 0,
    loanDuration: 0,
    loanStartDate: '',
    status: '',
    notes: '',
  };
  public collateral: CollateralDTO = {
    id: '',
    type: '',
    value: 0,
    share: 0,
  };
  public editCollateral: editcollateralDTO = {
    id: '',
    value: 0,
    share: 0,
  };

  constructor() {}
}
