import { HttpClient } from '@angular/common/http';
import { Injectable, ɵINJECTOR_IMPL__POST_R3__ } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  jwtHelper= new JwtHelperService();
  decodedToken:any;
baseUrl='/Accounts/'
constructor(private http:HttpClient) { }

  login(model:any){
  return this.http.post(this.baseUrl+'Login',model).pipe(
    map((response:any)=>{
        const user=response;
        if(user){
          localStorage.setItem('token',user.token);
          this.decodedToken=this.jwtHelper.decodeToken(user.token);
   console.log(this.decodedToken);
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
