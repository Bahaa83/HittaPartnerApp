import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output()cancelRegister=new EventEmitter;
  model: any={};
  registerForm!:FormGroup;
  constructor(private accountServices:AccountService,private alertify:AlertifyService) { }

  ngOnInit() {
    this.registerForm= new FormGroup(
      {
        userName:new FormControl(),
        password:new FormControl(),
        confirmPassword:new FormControl()

      }
    )
  }
  register(){
//this.accountServices.register(this.model).subscribe(
 // ()=>{this.alertify.success('du är medlem nu')},
  //error=>{ this.alertify.error(error)}
  
//)
  }
  cancel(){
this.cancelRegister.emit(false);
  }
  

}
