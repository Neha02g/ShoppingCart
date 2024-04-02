import { EventEmitter, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Route, Router } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { catchError, map, tap } from 'rxjs/operators';
import { throwError } from 'rxjs/internal/observable/throwError';



@Injectable({
  providedIn: 'root'
})
export class AuthService {

   // Define an event emitter to notify authentication changes
   authChanged: EventEmitter<boolean> = new EventEmitter<boolean>();
   
  constructor(private http :  HttpClient,private router:Router) { }


  login(data: any) {
    return this.http.post("http://localhost:5117/api/Auth/Login", data)
      .pipe(
        catchError((error: any) => {
          // Handle error here
          return throwError(()=>new Error(error.error)); // Pass the error message to the subscriber
        })
      );
  }

  setLoginData(token:any,userEmail:string)
  {
    localStorage.setItem("token", token);
       localStorage.setItem("userEmail",userEmail);
       this.authChanged.emit(true);
  }

  logout() {
    localStorage.removeItem("token");
    localStorage.removeItem("userEmail");
   // this.cartItemsSubject.next(0);
    this.authChanged.emit(false);
    this.router.navigate(['']);

  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  setUserEmail(email: string): void {
    localStorage.setItem("userEmail", email); // Store user's email in local storage
  }

  getUserEmail(){
    return localStorage.getItem("userEmail");
  }

  signup(data: any): Observable<any> {
    return this.http.post<any>("http://localhost:5117/api/Auth/Register", data)
    .pipe(
      catchError((error: any) => {
        // Handle error here
        return throwError(()=>new Error(error.error)); // Pass the error message to the subscriber
      })
    );;
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // Return an observable with a user-facing error message.
    return throwError(() => new Error('Something bad happened; please try again later.'));
  }




}
