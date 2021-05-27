import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AccountService } from './_services/account.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  jwtHelper= new JwtHelperService();
  constructor(public authService:AccountService){}
  ngOnInit() {
    const token = localStorage.getItem('token');
    this.authService.decodedToken= this.jwtHelper.decodeToken(token||'');
  }
}
