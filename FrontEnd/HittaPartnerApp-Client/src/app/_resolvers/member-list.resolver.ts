import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router } from "@angular/router";
import { empty, EMPTY, Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { PaginationResult } from "../_models/pagination";
import { User } from "../_models/user";
import { AlertifyService } from "../_services/alertify.service";
import { UserService } from "../_services/user.service";

@Injectable()
export class MemberListResolver implements Resolve<PaginationResult<User[]>>{
    pageNumber=1;
    pageSize=6;
    user:User=JSON.parse(localStorage.getItem('user')!)
    userParams:any={};
    constructor(private userService:UserService,private router:Router,private alertify:AlertifyService) {
        
    }
    resolve(route:ActivatedRouteSnapshot):Observable <PaginationResult<User[]>>{
        this.userParams.gender=this.user.gender==='Man'?'Kvinna':'Man';
    this.userParams.minAge=18;
    this.userParams.maxAge=99;
        return this.userService.getAllUsers(this.pageNumber,this.pageSize,this.userParams).pipe(
            catchError(error=>{
                this.alertify.error("Det är ett problem att visa data");
                this.router.navigate(['']);
                return of
            })
        )
    }
}

