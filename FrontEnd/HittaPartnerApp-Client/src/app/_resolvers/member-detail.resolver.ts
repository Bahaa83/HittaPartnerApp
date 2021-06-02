import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router } from "@angular/router";
import { EMPTY, Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { User } from "../_models/user";
import { AlertifyService } from "../_services/alertify.service";
import { UserService } from "../_services/user.service";

@Injectable()
export class MemberDetailResolver implements Resolve<User> {
    constructor(private userService:UserService,private router:Router,private alertify:AlertifyService) {
        
    }
    resolve(route:ActivatedRouteSnapshot):Observable<User>{
        return this.userService.getUserById(route.params['id']).pipe(
            catchError(()=>{
                this.alertify.error("Det är ett problem att visa data");
                this.router.navigate(['/member']);
                return EMPTY;

            })
        )
    }
}

