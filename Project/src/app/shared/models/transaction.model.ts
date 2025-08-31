import { Car } from "./car.model";

export class Transaction {
    public boughtCar?: Car;
    public purchaseDate?: Date = new Date();
    public amount?: number;
}
