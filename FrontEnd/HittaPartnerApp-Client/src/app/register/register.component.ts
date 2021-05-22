import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output()cancelRegister=new EventEmitter;
model: any={}
  constructor(private accountServices:AccountService) { }

  ngOnInit() {
  }
  register(){
this.accountServices.register(this.model).subscribe(
  ()=>{console.log('du är medlem nu')},
  error=>{console.log(error)}
  
)
  }
  cancel(){
this.cancelRegister.emit(false);
  }
  

}
