import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  user: User | undefined;
  constructor(private userService : UserService,private alertify:AlertifyService,private rout:ActivatedRoute) { }

  ngOnInit() {
    this.loadUser();
  }
  loadUser()
  {
    this.userService.getUserById(this.rout.snapshot.params['id']).subscribe(
      (user:User)=>{this.user=user},
      error=>{this.alertify.error(error)}
      
    )

  }

}
