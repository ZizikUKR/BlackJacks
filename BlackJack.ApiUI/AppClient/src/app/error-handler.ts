import { HttpEvent, 
    HttpInterceptor, 
    HttpHandler, 
    HttpRequest, 
    HttpResponse,
    HttpErrorResponse} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';


@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {   
    
    constructor(private router: Router){
    }
    
intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>{
return next.handle(request)
  .pipe(
    catchError( (error: HttpErrorResponse) => { 
       let errMsg = '';
       // Client Side Error
       if (error.error instanceof ErrorEvent) {        
         errMsg = `Error: ${error.error.message}`;
       } 
       else { 
        this.router.navigate(
            ["error"]
          );
       }       
       return throwError(errMsg);     
     })
  )
}
}   