import { Component, OnInit } from '@angular/core';
import { MovieService } from '../core/services/movie.service';
import { MovieCard } from '../shared/models/moviecard';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  movieCards!: MovieCard[]; //property we export to the view
  // ! is a TypeScript safety check, to make it nullable.
  constructor(private movieService: MovieService) { }   //DI

  ngOnInit(): void {
    // ngOnInit is one of the most important life cycle hooks method in Angular
    // It is recommended to use this method to call the API and initilize any data properties
    // This method will be called automatically by your Angular component after calling constructor

    // only when you subscribe to the observable, you get the data
    // When you subscribe to Observable<MovieCard[]>, it will unwrap it and we can get MovieCard[] (whatever inside the observable)
    // http://localhost:4200/ => HomeComponent
    this.movieService.getTopRevenueMovies().subscribe(
      m => {
        this.movieCards = m;      //initialize the property movieCards
        // To print an array of items in console window, use console.table instead of console.log.
      }
    );
  }

}
