import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import {  FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
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
 
  constructor(private formBuilder:FormBuilder, private accountServices:AccountService,private alertify:AlertifyService) {
   
   }

  ngOnInit() {
    this.registerForm = this.formBuilder.group(
      {
        userName:['',Validators.required],
        password:['',[Validators.required,Validators.minLength(4),Validators.maxLength(8)]],
        confirmPassword:['',Validators.required]

      },
      {
        validator:this.passwordMatcValidetorr
      }
      )

    
  }
 
   passwordMatcValidetorr (form:FormGroup)
   {
    return form.controls['password'].value === form.controls['confirmPassword']?.value?null
    :{'mismatch':true}
    
     
    
    }

  
  register(){
//this.accountServices.register(this.model).subscribe(
 // ()=>{this.alertify.success('du är medlem nu')},
  //error=>{ this.alertify.error(error)}
  
//)
console.log(this.registerForm.value);
  }
  cancel(){
this.cancelRegister.emit(false);
  }
  

}
function password(password: any) {
  throw new Error('Function not implemented.');
}

