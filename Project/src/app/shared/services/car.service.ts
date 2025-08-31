import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { BaseCar, Car } from '../models/car.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CarService {
  private url: string = environment.apiURL + "/Car";

  constructor(private httpClient: HttpClient) {}

  addCar(car: BaseCar): Observable<HttpResponse<{ message: string }>> {
    return this.httpClient.post<{ message: string }>(`${this.url}/Add`, car, { observe: 'response' });
  }

  getAllCars(): Observable<HttpResponse<Car[]>> {
    return this.httpClient.get<Car[]>(`${this.url}/Get-all`, { observe: 'response' });
  }

  getCarPaginated(pageNumber: number, pageSize: number): Observable<HttpResponse<Car[]>> {
    return this.httpClient.get<Car[]>(`${this.url}/Get-paginated?pageNumber=${pageNumber}&pageSize=${pageSize}`, { observe: 'response' });
  }

  getCarById(id: number): Observable<HttpResponse<Car>> {
    return this.httpClient.get<Car>(`${this.url}/${id}`, { observe: 'response' });
  }

  searchCar(value: string): Observable<HttpResponse<Car[]>> {
    return this.httpClient.get<Car[]>(`${this.url}/Search?value=${value}`, { observe: 'response' });
  }

  updateCar(id: number, car: BaseCar): Observable<HttpResponse<{ message: string }>> {
    return this.httpClient.put<{ message: string }>(`${this.url}/Update/${id}`, car, { observe: 'response' });
  }

  deleteCar(id: number): Observable<HttpResponse<{ message: string }>> {
    return this.httpClient.delete<{ message: string }>(`${this.url}/Delete/${id}`, { observe: 'response' });
  }
}
