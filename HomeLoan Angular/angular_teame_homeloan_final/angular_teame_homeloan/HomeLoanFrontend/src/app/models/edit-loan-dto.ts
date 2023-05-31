export class EditLoanDto {
    id:string;
    address:string;
    size:number;
    cost:number;
    registrationCost:number;
    monthlyFamilyIncome:number;
    otherIncome:number;
    loanAmount:number;
    loanDuration:number;
    loanStartDate:Date;
    
    constructor(){
        this.id='';
        this.address='';
        this.size=0;
        this.cost=0;
        this.registrationCost=0;
        this.monthlyFamilyIncome=0;
        this.otherIncome=0;
        this.loanAmount=0;
        this.loanDuration=0;
        this.loanStartDate=null;
    }
}
