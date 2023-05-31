export class ApplyCollateralDto {
    type: string;
    value: number;
    share: number;

    constructor()
    {
        this.type='';
        this.value=0;
        this.share=0;
    }

}
