import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router } from "@angular/router";
import { empty, EMPTY, Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { Message } from "../_models/message";
import { PaginationResult } from "../_models/pagination";
import { User } from "../_models/user";
import { AccountService } from "../_services/account.service";
import { AlertifyService } from "../_services/alertify.service";
import { UserService } from "../_services/user.service";

@Injectable()
export class MessageResolver implements Resolve <PaginationResult< Message[]>>{
    pageNumber = 1
    pageSize = 6
    messageType = 'Unread'
    constructor(private authService: AccountService, private userService: UserService, private router: Router, private alertify: AlertifyService) {
    }
    resolve(route: ActivatedRouteSnapshot): Observable<PaginationResult<Message[]>> {

        return this.userService.getMessgaes(this.authService.decodedToken.nameid, this.pageNumber, this.pageSize,
            this.messageType).pipe(
                catchError(error => {
                    this.alertify.error("Det är ett problem att visa data");
                    this.router.navigate(['']);
                    return of;
                })
            );
    }
}