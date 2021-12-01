import { Component, OnInit } from '@angular/core';
import { Login } from 'src/app/shared/models/login';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  userLogin: Login = {
    email: '',
    password: ''
  }

  constructor(private authService: AuthenticationService, private router: Router) { }

  ngOnInit(): void {
  }

  loginSubmit(){
    // When you click the submit button, capture the email/password from the view.
    // Then send the model to Authentication Service.

    // if token is saved successfully, then redirect to home page
    // if error then show error message and stay on same page

    this.authService.login(this.userLogin).subscribe(
      (response) => {
        if(response){
          this.router.navigateByUrl('/');
        }
        (err: HttpErrorResponse) => {
          console.log(err);
        }
      }
    )

  }

}
