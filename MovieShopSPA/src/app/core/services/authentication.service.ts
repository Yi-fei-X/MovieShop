import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { Login } from 'src/app/shared/models/login';
import { User } from 'src/app/shared/models/user';
import { environment } from 'src/environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private currentUserSubject = new BehaviorSubject<User>({} as User);
  public currentUser = this.currentUserSubject.asObservable();  // Outside class can only read this data, not modify.

  private isLoggedInSubject = new BehaviorSubject<boolean>(false);
  public isLoggedIn = this.isLoggedInSubject.asObservable();
  private jwtHelper = new JwtHelperService();

  constructor(private http: HttpClient) { }

  login(userLogin: Login): Observable<boolean>{
    // take email/password from login component and post it to api/account/login URL
    // if we get 200 OK status from API, which means email/password is correct, so we get token from API
    // store the token in localstorage
    // return true to component
    return this.http.post(`${environment.apiBaseUrl}account/login`, userLogin).pipe( map( (response: any) => {
      if(response){
        // save the response token(JWT) to local storage
        localStorage.setItem('token', response.token);
        // create the observables(RXJS Subject) so that other components can get notification when user successfully logged in
        // Any component can subscribe to this observable to get the notification
        this.populateUserInfo();
        return true;
      }
      return false;
    } ));

  }
  populateUserInfo() {
    // get the token from local storage
    var token = localStorage.getItem('token');
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      // decode the token (only when token is not empty and token is not expired) and get the information and put it inside user subject
      const decodedToken = this.jwtHelper.decodeToken(token)

      // set current user data into Observable
      this.currentUserSubject.next(decodedToken);

      // set is authenticated to true
      this.isLoggedInSubject.next(true);
    }  
    
  }

  logout(){
    // remove the token from local storage
    localStorage.removeItem('token');

    // reset the observables to initial values
    this.currentUserSubject.next({} as User);
    this.isLoggedInSubject.next(false);

  }

  register(){
    // take the user registration info model (firstName, lastName, dateOfBirth, email, password)
    // post it to api/account
    // if success, redirect to login route

  }
}
