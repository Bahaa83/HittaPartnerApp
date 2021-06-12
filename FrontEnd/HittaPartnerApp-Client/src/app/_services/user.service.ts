import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PaginationResult } from '../_models/pagination';
import { User } from '../_models/user';



@Injectable({
  providedIn: 'root'
})
      export class UserService {
      baseUrl=environment.apiUrl+'Users/';
      photobaseUrl=environment.apiUrl;
      constructor(private http:HttpClient) { }
      getAllUsers(page: number|null,itemsPerPage:number|null,userParams:any|null):Observable<PaginationResult<User[]>>
      {
       const paginationResult:PaginationResult<User[]>= new PaginationResult<User[]>();
       let params= new HttpParams();
       if(page!=null && itemsPerPage!=null){
         params=params.append('pageNumber',page);
         params= params.append('pageSize',itemsPerPage);
         
       }
       if(userParams!=null){
        params=params.append('minAge',userParams.minAge);
        params= params.append('maxAge',userParams.maxAge);
        params= params.append('gender',userParams.gender);
        params= params.append('orderBy',userParams.orderBy);
       }
      return this.http.get<User[]|any>(this.baseUrl+'GetAllUsers',{observe:'response',params}).pipe(
        map(response=>{
          paginationResult.result=response.body;
          if(response.headers.get('Pagination')!=null){
            paginationResult.pagination= JSON.parse(response.headers.get('Pagination')!)
          }
          return paginationResult;
        })
      );
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
            deletePhoto(userId:string,photoId:string){
              return this.http.delete(this.photobaseUrl+'Photos/DeletePhoto?userId='+userId+'&photoId='+photoId)
            }

            sendLike(id:string,recipientId:string){
              return this.http.post(this.baseUrl+'SendLike?id='+id+'&recipientId='+recipientId,{});
            }
            
           
 }
