import { HttpClient } from '@angular/common/http';
import { Injectable, ɵINJECTOR_IMPL__POST_R3__ } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from "rxjs/operators";
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  jwtHelper= new JwtHelperService();
  decodedToken:any;
 baseUrl=environment.apiUrl+'Accounts/';
 currentUser!:User;
constructor(private http:HttpClient) { }

  login(model:any){
  return this.http.post(this.baseUrl+'Login',model).pipe(
    map((response:any)=>{
        const user=response;
        if(user){
          localStorage.setItem('token',user.token);
          localStorage.setItem('user',JSON.stringify( user.user))
          this.decodedToken=this.jwtHelper.decodeToken(user.token);
          this.currentUser=user.user;
        }
    }))
  
  }
register(model:any){
  return this.http.post(this.baseUrl+'Register',model)
}
logedIn()
{
  try{
    const token=localStorage.getItem('token');
  return ! this.jwtHelper.isTokenExpired(token||'');
  }
  catch{
    return false;
  }
}
}
