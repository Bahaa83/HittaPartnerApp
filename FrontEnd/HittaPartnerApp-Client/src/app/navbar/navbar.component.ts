import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { AlertifyService } from '../_services/alertify.service';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
model:any={};
photoUrl!:string;
  constructor(public authService:AccountService,private altertify:AlertifyService,private router:Router) { }

  ngOnInit() {
    this.authService.currentPhotoUrl.subscribe(
      photoUrl=>this.photoUrl=photoUrl
    )
  }
  login(){
    this.authService.login(this.model).subscribe(
      next=>{this.altertify.success('Logga in framgångsrikt');},
      error=>{this.altertify.error(error);},
      ()=>{this.router.navigate(['members'])}
    )
  }
// Fonktion som hämtar tilbacka token som vi här fåt tillbaka med inloggnings reguest från local storage,
// om token är not null retunerar true annars fals.
  loggedIn(){
  return this.authService.logedIn();
  }
  loggedOut(){
    localStorage.removeItem('token');
    this.authService.decodedToken=null;
    localStorage.removeItem('user');
    this.authService.currentUser!=null;
    this.altertify.warning('Du är utloggad');
    this.router.navigate(['home']);
  }
}
