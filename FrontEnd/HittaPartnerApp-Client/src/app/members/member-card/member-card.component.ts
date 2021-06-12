import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
  @Input()
  user!: User; 
  constructor(private uthService:AccountService, private userService :UserService,private alertify:AlertifyService) { }

  ngOnInit() {

  }
  sendLike(id:string){
    this.userService.sendLike(this.uthService.decodedToken.nameid,id).subscribe(
      data=>{this.alertify.success('Du gillade '+this.user.knownAs)},
      error=>{this.alertify.error(error)}
    )
  }
}
