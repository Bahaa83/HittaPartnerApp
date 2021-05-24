import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HTTP_INTERCEPTORS } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";

@Injectable()
export class HttperorInterceptorServices implements HttpInterceptor{
    intercept(req:HttpRequest<any>,next:HttpHandler):Observable<HttpEvent<any>>{
        return next.handle(req).pipe(
            catchError((error:HttpErrorResponse)=>{
                const errorMessage= this.setError(error);
                    console.log(errorMessage);
                    return throwError(errorMessage);
                
            })

        );
            
    }  
    setError(error:HttpErrorResponse):string{
        let errorMessage='Unknown error occured';
        if(error.error instanceof ErrorEvent){
            //Client side error
            errorMessage=error.error.message;
            
        }
           else{
               //Server side error
               if(error.status!==0){
            errorMessage=error.error;
               }
        }
        return errorMessage
        
    }
}






export  const ErrorInceptorProvidor={
    provide:HTTP_INTERCEPTORS,
    useClass: HttperorInterceptorServices,
    multi:true
}
