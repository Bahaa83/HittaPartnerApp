import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router } from "@angular/router";
import { empty, EMPTY, Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { User } from "../_models/user";
import { AccountService } from "../_services/account.service";
import { AlertifyService } from "../_services/alertify.service";
import { UserService } from "../_services/user.service";

@Injectable()
export class MemberEditResolver implements Resolve<User> {
    constructor(private authservice:AccountService, private userService:UserService,private router:Router,private alertify:AlertifyService) {
        
    }
    resolve(route:ActivatedRouteSnapshot):Observable<User>{
        return this.userService.getUserById(this.authservice.decodedToken.nameid).pipe(
            catchError(error=>{
                this.alertify.error("Det är ett problem att visa data");
                this.router.navigate(['/member']);
                return of
            })
        )
    }
}

