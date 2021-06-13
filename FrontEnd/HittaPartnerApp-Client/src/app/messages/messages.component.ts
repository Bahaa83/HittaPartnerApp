import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Message } from '../_models/message';
import { Pagination, PaginationResult } from '../_models/pagination';
import { AccountService } from '../_services/account.service';
import { AlertifyService } from '../_services/alertify.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
messages:Message[]|any;
pagination:Pagination|any;
messageType='Unread'
  constructor(private userService :UserService, private authService:AccountService,private route:ActivatedRoute
    ,private alertify :AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(
      data=>{
        this.messages= data['messages'].result;
        this.pagination= data['messages'].pagination;
      }
    )
  }
  loadMessages(){
    this.userService.getMessgaes(this.authService.decodedToken.nameid,this.pagination.currentPage,
      this.pagination.itemsPerPage,this.messageType).subscribe(
        (res:PaginationResult<Message[]>)=>{
          this.messages=res.result;
          this.pagination=res.pagination;
        },
        error=>this.alertify.error(error)
      )
  }
  pageChanged(event:any){
    this.pagination.currentPage=event.page;
    this.loadMessages();
  }

}
