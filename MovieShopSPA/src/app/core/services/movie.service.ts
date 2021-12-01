import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MovieCard } from 'src/app/shared/models/moviecard';
import { HttpClient } from '@angular/common/http'
import { Movie } from 'src/app/shared/models/movie';
import { environment } from 'src/environments/environment';

@Injectable({     //decorator
  providedIn: 'root'
})
export class MovieService {
  // In C# we do private readonly HttpClient _http;
  constructor(private http: HttpClient) { }   //Dependency Injection
  // http is variable name, 

  // https://localhost:44328/api/Movies/toprevenue
  // Services have many methods that will be used by components. (Like C# controller will call methods in services)
  
  // HomeComponent will call this function
  getTopRevenueMovies(): Observable<MovieCard[]>{
    // call our API, using HttpClient (XMLHttpRequest) to make GET request.
    // HttpClient class comes from HttpClientModule (Angular Team created for us to use)
    // first import HttpClientModule inside AppModule

    // read the base API Url from the environment file and then append the needed URL per method
    return this.http.get<MovieCard[]>(`${environment.apiBaseUrl}movies/toprevenue`);    //remember to convert the return type to what you want  

  }

  getMovieDetails(id:number) : Observable<Movie>{
    // To avoid hard coding the domain name, we need to configure it (changeable)
    // In C#, appsetting.json is our configuration file
    // In angular, we have environment.ts as our configuration file. 
    // https://localhost:5001/api/movies/3
    return this.http.get<Movie>(`${environment.apiBaseUrl}movies/${id}`);
  }
}
