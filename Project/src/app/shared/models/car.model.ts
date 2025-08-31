export class BaseCar {
    public imgUrl1?: string;
    public imgUrl2?: string;
    public manufacturer?: string;
    public model?: string;
    public year?: number;
    public color?: string;
    public price?: number;
    public mileage?: number;
    public details?: CarDetails;
}

export class Car extends BaseCar {
    public id!: number;
}


export class CarDetails {
    public fuelType?: string;
    public transmission?: string;
    public numberOfDoors?: number;
    public hasSunroof?: boolean;
    public driveTrain?: string;
    public lastServiceDate?: Date;
}