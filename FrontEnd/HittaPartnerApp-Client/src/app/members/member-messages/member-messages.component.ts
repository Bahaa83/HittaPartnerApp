import { Component, Input, OnInit } from '@angular/core';
import { Message } from 'src/app/_models/message';
import { AccountService } from 'src/app/_services/account.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit {
@Input() recipientId:string|any;
messages:Message[]|any;
newMessage:any={};
  constructor(private authService:AccountService,private userService:UserService,private alertify:AlertifyService) { }

  ngOnInit(): void {
    this.loadMessages();
  }
  loadMessages(){
    this.userService.getConversation(this.authService.decodedToken.nameid,this.recipientId).subscribe(
      messages=>{
        this.messages=messages;
      },
      error=>{this.alertify.error(error);}
    )
  }
  sendMessage(){
    this.newMessage.recipientId= this.recipientId;
    this.userService.sendMessage(this.authService.decodedToken.nameid,this.newMessage).subscribe(
     ( message:Message|any)=>{this.messages.push(message);
      this.newMessage.content='';
    }
    )
  }
}
