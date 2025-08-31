import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Transaction } from '../models/transaction.model';

@Injectable({
  providedIn: 'root'
})
export class BuyCarService {
  private url: string = environment.apiURL + "/BuyCar";

  constructor(private httpClient: HttpClient) {}

  buyCar(userId: number, carId: number): Observable<HttpResponse<{ message: string }>> {
    return this.httpClient.post<{ message: string }>(`${this.url}/User/${userId}/Car/${carId}`, null, { observe: 'response' });
  }

  buyCarFromFavourites(userId: number, carId: number): Observable<HttpResponse<{ message: string }>> {
    return this.httpClient.post<{ message: string }>(`${this.url}/Favourites/User/${userId}/Car/${carId}`, null, { observe: 'response' });
  }

  getTransactions(userId: number): Observable<HttpResponse<Transaction[]>> {
    return this.httpClient.get<Transaction[]>(`${this.url}/User/${userId}/Transactions`, { observe: 'response' });
  }
}