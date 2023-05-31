export class CollateralListDto {
    id:string;
    type:string;
    value:number;
    share: number;
    checked:boolean;

    constructor(){
        this.id='';
        this.type='';
        this.value=0;
        this.share=0;
        this.checked=false;
    }
}
