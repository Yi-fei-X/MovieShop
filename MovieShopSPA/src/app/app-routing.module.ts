import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './account/login/login.component';
import { AuthGuard } from './core/guards/auth.guard';
import { AuthenticationService } from './core/services/authentication.service';
import { HomeComponent } from './home/home.component';

// specify all the routes required by the angular application
const routes: Routes = [
  { path: "", component: HomeComponent },    // path/route for my home page http://localhost:4200/
  { path: 'user', loadChildren:() => import('./user/user.module').then(mod => mod.UserModule), canLoad:[AuthGuard]},

  // lazily load the modules, define main route for lazy modules
  {
    path: "movies", loadChildren: () => import("./movies/movies.module").then(mod => mod.MoviesModule)
  },
  
  { path: "account/login", component: LoginComponent }

  // {path: "movies/:id", component:MovieDetailsComponent }
  // {path: "admin/createmovie", component: CreateMovieComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
