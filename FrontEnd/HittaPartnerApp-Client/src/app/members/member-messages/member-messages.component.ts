import { AfterViewChecked, Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { Message } from 'src/app/_models/message';
import { AccountService } from 'src/app/_services/account.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit ,AfterViewChecked{
@Input() recipientId:string|any;
@ViewChild('panel',{static:true}) panel:ElementRef|any
;messages:Message[]|any;
hubConnection!:signalR.HubConnection;
newMessage:any={};
  constructor(private authService:AccountService,private userService:UserService,private alertify:AlertifyService) { }
  ngAfterViewChecked(): void {
   this.panel.nativeElement.scrollTop= this.panel.nativeElement.scrollHeight
  }

  ngOnInit(): void {
    this.loadMessages();
    this.hubConnection= new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:44350/chat",{
    skipNegotiation:true,
    transport:signalR.HttpTransportType.WebSockets}).build();
    this.hubConnection.start();
    this.hubConnection.on('refresh',()=>{
      setTimeout(() => {
        this.loadMessages();
      }, 1000);
    })
  }
  loadMessages(){
    this.userService.getConversation(this.authService.decodedToken.nameid,this.recipientId).subscribe(
      messages=>{
        this.messages= messages.reverse();
      },
      error=>{this.alertify.error(error);}
    )
  }
  sendMessage(){
    this.newMessage.recipientId= this.recipientId;
    this.userService.sendMessage(this.authService.decodedToken.nameid,this.newMessage).subscribe(
     ( message:Message|any)=>{
     
      this.messages.push(message);
      this.newMessage.content='';
      this.hubConnection.invoke('refresh');
    }
    )
  }
}
