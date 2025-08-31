import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';
import { Car } from '../models/car.model';

@Injectable({
  providedIn: 'root'
})
export class SortCarsService {

  private apiUrl = environment.apiURL + '/SortCars';

  constructor(private httpClient: HttpClient) { }

  sortByManufacturer(manufacturer: string): Observable<Car[]> {
    return this.httpClient.get<Car[]>(`${this.apiUrl}/${manufacturer}`);
  }

  orderByAscending(): Observable<Car[]> {
    return this.httpClient.get<Car[]>(`${this.apiUrl}/Order-by-price-ascending`);
  }

  orderByDescending(): Observable<Car[]> {
    return this.httpClient.get<Car[]>(`${this.apiUrl}/Order-by-price-descending`);
  }

  sortByDrivetrain(drivetrain: string): Observable<Car[]> {
    return this.httpClient.get<Car[]>(`${this.apiUrl}/Drive-trains/${drivetrain}`);
  }

  sortByFuelType(fuelType: string): Observable<Car[]> {
    return this.httpClient.get<Car[]>(`${this.apiUrl}/Fuel-types/${fuelType}`);
  }

  sortByTransmission(transmission: string): Observable<Car[]> {
    return this.httpClient.get<Car[]>(`${this.apiUrl}/Transmissions/${transmission}`);
  }
}
