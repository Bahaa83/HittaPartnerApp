import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Message } from '../_models/message';
import { PaginationResult } from '../_models/pagination';
import { User } from '../_models/user';



@Injectable({
  providedIn: 'root'
})
      export class UserService {
      baseUrl=environment.apiUrl+'Users/';
      photobaseUrl=environment.apiUrl;
      messagebaseUrl=environment.apiUrl;
      constructor(private http:HttpClient) { }
      getAllUsers(page?: number,itemsPerPage?:number,userParams?:any,likeParam?:string):Observable<PaginationResult<User[]>>
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
       if(likeParam ==='Likers'){
         params= params.append('likers','true');
       }
       if(likeParam ==='Likees'){
        params= params.append('likees','true');
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
            getMessgaes(id:string,page?:any,itemsPerPage?:any,messageType?:any){
              const paginationResult:PaginationResult<Message[]>= new PaginationResult<Message[]>();
              let params=new HttpParams();
              params=params.append('MessageType',messageType);
              if(itemsPerPage!=null&&page!=null){
                
                params=params.append('pageNumber',page);
                params= params.append('pageSize',itemsPerPage);
                
              }
              return this.http.get<Message[]|any>(this.messagebaseUrl+'Messages/GetMessagesForUser?userId='+id,{observe:'response',params}).pipe(
                map(response=>{
                  paginationResult.result=response.body;
                  if(response.headers.get('Pagination')!==null){
                    paginationResult.pagination= JSON.parse(response.headers.get('Pagination')!);

                  }
                  return paginationResult;
                })
              );
             
            }

            getConversation(id:string,recipientid:string){
              return this.http.get<Message[]>(this.messagebaseUrl+'Messages/GetConversation?userid='+id+'&recipientId='+recipientid);
            }
            
 }
