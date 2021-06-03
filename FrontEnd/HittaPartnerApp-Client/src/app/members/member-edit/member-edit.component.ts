import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
user!:User;
@ViewChild ('editForm') editForm:NgForm | undefined
  constructor(private route:ActivatedRoute,private alertify:AlertifyService) { }

  ngOnInit(): void {
    this.route.data.subscribe(data=>{
      this.user= data['user'];
    })
  }
  updateUser()
  {
    this.editForm?.reset(this.user)
    this.alertify.success("Profilen har ändrats")
  }

}
