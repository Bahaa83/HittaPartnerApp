import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';

const httpOptios={
  headers:new HttpHeaders(
    {
      'Authorization':'Bearer '+localStorage.getItem('token')
    }
  )
};
@Injectable({
  providedIn: 'root'
})
export class UserService {
baseUrl=environment.apiUrl+'Users/';
constructor(private http:HttpClient) { }
getAllUsers():Observable<User[]>
{
return this.http.get<User[]>(this.baseUrl+'GetAllUsers',httpOptios);
}
getUserById(id:string):Observable<User>
{
return this.http.get<User>(this.baseUrl+'GetUserByID'+id,httpOptios);
}
}
