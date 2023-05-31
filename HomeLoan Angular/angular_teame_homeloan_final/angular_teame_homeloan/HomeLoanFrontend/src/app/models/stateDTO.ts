export default class stateDTO
{
    id:any;
    code:string;
    name:string;
    countryCode:string;
    constructor(id:any,code:string,name:string,countryCode:string)
    {
        this.id=id;
        this.code=code;
        this.name=name;
        this.countryCode=countryCode;
    }
}