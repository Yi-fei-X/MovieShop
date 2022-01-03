import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Movie } from 'src/app/shared/models/movie';
import { MovieCard } from 'src/app/shared/models/moviecard';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  // unfinished method
  getMoviePurchases(id: number): Observable<MovieCard[]>{
    // call API method to get the purchased movies
    // send token with http header
    // In Angular we use a class called HttpHeader

    // pseudo code:
    // var token = localStorage.getItem('token');
    // headers.set('Authorization', `Bearer ${token} `)
    return this.http.get<MovieCard[]>(`${environment.apiBaseUrl}users/purchases`);
  }
}
