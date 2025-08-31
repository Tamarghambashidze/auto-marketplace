import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class CarDetailsService {

  private url: string = environment.apiURL + "/CarDetails";

  constructor(private httpClient: HttpClient) { }

  getManufacturers(): Observable<string[]> {
    return this.httpClient.get<string[]>(`${this.url}/Manufacturers`);
  }

  getTransmissions(): Observable<string[]> {
    return this.httpClient.get<string[]>(`${this.url}/Transmissions`);
  }

  getFuelTypes(): Observable<string[]> {
    return this.httpClient.get<string[]>(`${this.url}/Fuel-types`);
  }

  getDriveTrains(): Observable<string[]> {
    return this.httpClient.get<string[]>(`${this.url}/Drive-trains`);
  }
}

