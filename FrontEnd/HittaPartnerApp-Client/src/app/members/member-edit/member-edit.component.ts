import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { UserService } from 'src/app/_services/user.service';

    @Component({
      selector: 'app-member-edit',
      templateUrl: './member-edit.component.html',
      styleUrls: ['./member-edit.component.css']
    })
    export class MemberEditComponent implements OnInit {
    user!:User;
    photoUrl!:string;
    @ViewChild ('editForm') editForm:NgForm | undefined;
    @HostListener('window:beforeunload',['$event'])
    unLoadNotification($event:any){
    if(this.editForm?.dirty){
    $event.returnValue=true;
    }
    }
      constructor(private route:ActivatedRoute,private alertify:AlertifyService,private userService:UserService,private authService:AccountService) { }

      ngOnInit(): void {
        this.route.data.subscribe(data=>{
          this.user= data['user'];
        });
        this.authService.currentPhotoUrl.subscribe(photoUrl=>this.photoUrl=photoUrl);
      }
      updateUser()
      {
        this.editForm?.reset(this.user)
        this.userService.updateUser(this.authService.decodedToken.nameid,this.user).subscribe(
          next=>{ this.alertify.success("Profilen har ändrats")},
          error=>{ this.alertify.error(error)}
        )
      }

    }
