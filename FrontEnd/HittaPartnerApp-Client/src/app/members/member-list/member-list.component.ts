import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PaginationComponent } from 'ngx-bootstrap/pagination';
import { Pagination, PaginationResult } from 'src/app/_models/pagination';
import { User } from '../../_models/user';
import { AlertifyService } from '../../_services/alertify.service';
import { UserService } from '../../_services/user.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  
 users!: User[];
 user:User=JSON.parse(localStorage.getItem('user')!)
 genderList=[{value:'Man',display:'Män'},{value:'Kvinna',display:'Kvinnor'}]
 userParams:any={};
 pagination !: Pagination 
  constructor(private userService:UserService,private alertify:AlertifyService,private route:ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(
      data=>{
        this.users=data['users'].result;
        this.pagination= data['users'].pagination;
        
      }
    );
    this.userParams.gender=this.user.gender==='Man'?'Kvinna':'Man';
    this.userParams.minAge=18;
    this.userParams.maxAge=99;
    this.userParams.orderBy='lastActive';
   
  }
  resetFilter(){
    this.userParams.gender=this.user.gender==='Man'?'Kvinna':'Man';
    this.userParams.minAge=18;
    this.userParams.maxAge=99;
    this.loadUsers();
  }
  pageChanged(event:any){
    this.pagination.currentPage=event.page;
    this.loadUsers();
  }
  loadUsers(){
   this.userService.getAllUsers(this.pagination.currentPage,this.pagination.itemsPerPage,this.userParams)
   .subscribe((users:PaginationResult<User[]>)=>
    {
      this.users=users.result;
      this.pagination=users.pagination;
    },
   error=> {this.alertify.error(error)}
    )
  }

}
