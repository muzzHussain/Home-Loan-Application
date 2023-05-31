export default class cityDTO
{
    id:any;
    code:string;
    name:string;
    stateCode:string;
    constructor(id:any,code:string,name:string,stateCode:string)
    {
        this.id=id;
        this.code=code;
        this.name=name;
        this.stateCode=stateCode;
    }
}