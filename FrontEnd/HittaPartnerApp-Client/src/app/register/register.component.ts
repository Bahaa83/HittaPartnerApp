import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import {  FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output()cancelRegister=new EventEmitter;
 user:User | undefined
  registerForm:FormGroup=new FormGroup({})
 
  constructor(private router:Router, private fb:FormBuilder, private accountServices:AccountService,private alertify:AlertifyService) { }

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
    if(this.registerForm.valid)
    {
         this.user=Object.assign({},this.registerForm.value);

         this.accountServices.register(this.user!).subscribe(
          ()=>{this.alertify.success('du ??r medlem nu')},
          error=>{ this.alertify.error(error)},
          ()=>{this.accountServices.login(this.user).subscribe(
             ()=>{this.router.navigate(['members'])}
          )}
          
        )
    }

  

  }
  cancel(){
this.cancelRegister.emit(false);
  }
  

}
function password(password: any) {
  throw new Error('Function not implemented.');
}

