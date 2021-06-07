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
  registerForm:FormGroup=new FormGroup({})
 
  constructor(private fb:FormBuilder, private accountServices:AccountService,private alertify:AlertifyService) { }

  ngOnInit() {
  
   this. createRegisterForm();
  }
  createRegisterForm()
  {
    this.registerForm = this.fb.group(
      {
        gender:['Man'],
        userName: ['', Validators.required],
        knownAs : ['',Validators.required],
        dateOfBirth:[null,Validators.required],
        city:['',Validators.required],
        country:['',Validators.required],
        password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
        confirmPassword: ['', Validators.required]

      },{validator:this.passwordMatcValidetor('password','confirmPassword')})
  }

    get get(){
      return this.registerForm.controls;
    }

   passwordMatcValidetor (password:string,confirmPassword:string)
   {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls[password];
      const matchingControl = formGroup.controls[confirmPassword];
      if (matchingControl.errors && !matchingControl.errors.confirmedValidator) {
          return;
      }
      if (control.value !== matchingControl.value) {
          matchingControl.setErrors({ 'mismatch': true });
      } else {
          matchingControl.setErrors(null);
      }
  }
    
     
    
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

