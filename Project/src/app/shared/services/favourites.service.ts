import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';
import { Car } from '../models/car.model';

@Injectable({
  providedIn: 'root'
})
export class FavouritesService {

  private apiUrl = environment.apiURL + '/Favourites';

  constructor(private httpClient: HttpClient) { }

  addCarToFavourites(userId: number, carId: number): Observable<{ message: string }> {
    return this.httpClient.post<{ message: string }>(`${this.apiUrl}/${carId}/User/${userId}`, {});
  }

  getUserFavourites(userId: number): Observable<Car[]> {
    return this.httpClient.get<Car[]>(`${this.apiUrl}/${userId}/Favourites`);
  }

  deleteCarFromFavourites(userId: number, carId: number): Observable<{ message: string; carId: number }> {
    return this.httpClient.delete<{ message: string; carId: number }>(`${this.apiUrl}/User/${userId}/Favourites/Car/${carId}`);
  }
}