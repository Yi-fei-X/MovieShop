import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';    //import module here, it will be available fro all application
import { FormsModule } from '@angular/forms';     //after importing here, also import it in the imports part
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './core/layout/header/header.component';
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';
import { MovieCardComponent } from './shared/components/movie-card/movie-card.component';


@NgModule({     //decorator
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    LoginComponent,
    RegisterComponent,
    MovieCardComponent
  ],
  imports: [  // remember also import the module here
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
    //UserModule,
    //AdminModule,
    //AccountModule
    //MoviesModule    //if you list a module here, it will load immediately in the main.js when you run the code. Remove it from here if you want to lazily load it.
  ],
  providers: [],
  bootstrap: [AppComponent]   //nothing to do with bootstrap library, bootstrap here means starting up
})
export class AppModule { }    //without decorator it will be a normal TS class, with decorator it will be a Angular module
