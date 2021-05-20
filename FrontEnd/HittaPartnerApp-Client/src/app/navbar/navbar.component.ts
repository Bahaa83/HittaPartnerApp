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
    error=>{console.log('Kunde inte logga in');}
  )
}
}
