import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';


@Injectable({
  providedIn: 'root'
})
      export class UserService {
      baseUrl=environment.apiUrl+'Users/';
      photobaseUrl=environment.apiUrl;
      constructor(private http:HttpClient) { }
      getAllUsers():Observable<User[]>
      {
      return this.http.get<User[]>(this.baseUrl+'GetAllUsers');
      }
        getUserById(id:string):Observable<User>
        {
        return this.http.get<User>(this.baseUrl+'GetUserByID?userId='+id);
        }
          updateUser(id:string,user:User){
            return this.http.put(this.baseUrl+'UpdateUser?userID=' +id,user)
          }
            setMainPhoto(userId:string,photoId:string){
              return this.http.post(this.photobaseUrl+'Photos/SetMainPhotoForUser?userId='+userId+'&photoId='+photoId,{});
            }


 }
