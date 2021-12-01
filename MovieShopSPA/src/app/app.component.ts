import { Component } from '@angular/core';
import { AuthenticationService } from './core/services/authentication.service';

@Component({    //decorator
  selector: 'app-root',
  templateUrl: './app.component.html',    //the view that return for this component
  styleUrls: ['./app.component.css']
})
export class AppComponent {   //make this AppComponent from normal TS class to component by using component decorator
  title = 'MovieShopSPA';

  constructor(private authService: AuthenticationService){}
  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    this.authService.populateUserInfo();
  }

}
