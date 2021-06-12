import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Pagination, PaginationResult } from '../_models/pagination';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';
import { AlertifyService } from '../_services/alertify.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {
users!: User[];
pagination!:Pagination;
likeParam!:string;
search:boolean=false;
  constructor(private aythService:AccountService,private userService:UserService,private route:ActivatedRoute,
    private alertify:AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(
      data=>{
        this.users= data['users'].result;
        this.pagination= data['users'].pagination;
        
      }
    );
    this.likeParam='likers'
  }

  loadUsers(){
    if(!this.search){
      this.pagination.currentPage=1;
    }
   this.userService.getAllUsers(this.pagination.currentPage,this.pagination.itemsPerPage,null,this.likeParam )
   .subscribe((users:PaginationResult<User[]>)=>
    {
      this.users=users.result;
      this.pagination=users.pagination;
    },
   error=> {this.alertify.error(error)}
    )
  }
  pageChanged(event:any){
    this.pagination.currentPage=event.page;
    this.loadUsers();
  }
}



