import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
model:any={}
  constructor(private authService:AccountService) { }

  ngOnInit() {
  }
login(){
  this.authService.login(this.model).subscribe(
    next=>{console.log('Logga in framgångsrikt');},
    error=>{console.log(error);}
  )
}
// Fonktion som hämtar tilbacka token som vi här fåt tillbaka med inloggnings reguest från local storage,
// om token är not null retunerar true annars fals.
loggedIn(){
  const token=localStorage.getItem('token');
  return !!token
}
loggedOut(){
  localStorage.removeItem('token');
  console.log('Du är utloggad')
}
}
