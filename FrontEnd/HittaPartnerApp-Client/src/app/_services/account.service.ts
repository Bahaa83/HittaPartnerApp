import { HttpClient } from '@angular/common/http';
import { Injectable, ɵINJECTOR_IMPL__POST_R3__ } from '@angular/core';
import { map } from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
baseUrl='https://localhost:44350/api/Accounts/'
constructor(private http:HttpClient) { }

  login(model:any){
  return this.http.post(this.baseUrl+'Login',model).pipe(
    map((response:any)=>{
        const user=response;
        if(user){
          localStorage.setItem('token',user.token);
        }
    }))
  
  }
register(model:any){
  return this.http.post(this.baseUrl+'Register',model)
}

}
