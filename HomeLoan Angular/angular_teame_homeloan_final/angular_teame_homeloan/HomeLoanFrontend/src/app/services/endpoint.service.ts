import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class EndpointService {
  //Get

  public GetAllLoanApplicationByAdvisor: string =
    'https://localhost:44382/api/Loan/GetAllLoanApplicationByAdvisorTask';
  public FetchAnLoanApplicationByIdByAdvisor: string =
    'https://localhost:44382/api/Loan/GetAnLoanApplicationByAdvisorTask?applicationId=';
  public GetAllCollateral: string = 'https://localhost:44382/api/Collateral';
  public DeleteCollateral: string =
    'https://localhost:44382/api/Collateral?Id=';
  public editCollateralService: string =
    'https://localhost:44382/api/Collateral/EditCollateralActionTask';

  public getLocationService: string =
    'https://localhost:44382/api/CountryStateCity/GetLocation';
  public editCountryService: string =
    'https://localhost:44382/api/CountryStateCity/EditCountryActionTask';
  public editStateService: string =
    'https://localhost:44382/api/CountryStateCity/EditStateActionTask';
  public editCityService: string =
    'https://localhost:44382/api/CountryStateCity/EditCityActionTask';

  public GetAllLoanApplicationByAdvisorTask: string =
    'https://localhost:44382/api/Loan/GetAllAppliedLoanApplicationByAdvisorTask';

  public GetAnAppliedLoanApplicationByAdvisorTask: string =
    'https://localhost:44382/api/Loan/GetAnLoanApplicationByAdvisorTask?applicationId=';

  public ChangeLoanStatusByAdvisorTask: string =
    'https://localhost:44382/api/Loan/ChangeLoanStatusByAdvisorTask';
  constructor() {}
}
