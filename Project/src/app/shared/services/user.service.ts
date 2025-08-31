import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';
import { CreateUser, User } from '../models/user.model'; 
import { HttpResponse } from '@angular/common/http';
import { UserLogIn, UserPasswordUpdate } from '../models/user-log-in.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private url = environment.apiURL + '/User';  

  constructor(private httpClient: HttpClient) {}

  registerUser(userDto: CreateUser): Observable<HttpResponse<{ message: string }>> {
    return this.httpClient.post<{ message: string }>(`${this.url}/Registration`, userDto, { observe: 'response' });
  }

  logIn(userLogin: UserLogIn): Observable<HttpResponse<{ message: string, user: User, token: any }>> {
    return this.httpClient.post<{ message: string, user: User, token: any }>(`${this.url}/Log-in`, userLogin, { observe: 'response' });
  }

  getUserById(id: number): Observable<HttpResponse<User>> {
    return this.httpClient.get<User>(`${this.url}/${id}`, { observe: 'response' });
  }

  updateUser(id: number, userDto: CreateUser): Observable<HttpResponse<{ message: string }>> {
    return this.httpClient.put<{ message: string }>(`${this.url}/Update/${id}`, userDto, { observe: 'response' });
  }

  updatePassword(userPasswordUpdate: UserPasswordUpdate): Observable<HttpResponse<{ message: string }>> {
    return this.httpClient.put<{ message: string }>(`${this.url}/Update-password`, userPasswordUpdate, { observe: 'response' });
  }

  deleteUser(id: number, userLogin: UserLogIn): Observable<HttpResponse<{ message: string }>> {
    return this.httpClient.delete<{ message: string }>(`${this.url}/${id}`, { body: userLogin, observe: 'response' });
  }
}
