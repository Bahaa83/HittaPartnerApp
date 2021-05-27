import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HTTP_INTERCEPTORS } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { AlertifyService } from "./alertify.service";

@Injectable({
    providedIn:'root'
})
export class HttperorInterceptorServices implements HttpInterceptor{
    constructor (private alertify:AlertifyService){}
    intercept(req:HttpRequest<any>,next:HttpHandler){
        return next.handle(req).pipe(
            catchError((error:HttpErrorResponse)=>{
          const errorMessage= this.setError(error);
             this.alertify.warning(errorMessage);
             
               return throwError(errorMessage);
                    
                
            })

        );
            
    }  
    setError(error:HttpErrorResponse):string{
        let errorMessage='Unknown error occured';
        if(error.error instanceof ErrorEvent){
            //Client side error
            errorMessage=error.status.toString();
           
            
        }
            else
            {
                        if(error.status!==0)
                        {
                            //Server side error
                            
                            
                                errorMessage=error.error;
                            
                        }
            }
        return errorMessage;
        
    }
}






export  const ErrorInceptorProvidor={
    provide:HTTP_INTERCEPTORS,
    useClass: HttperorInterceptorServices,
    multi:true
}
