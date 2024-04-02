import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { ChangePasswordModel } from 'src/Models/changePasswordModel';

@Injectable({
  providedIn: 'root'
})
export class ForgotPasswordService {

  constructor(private http:HttpClient) { }

  sendOTP(email: string): Observable<any> {
    const url = `http://localhost:5117/api/User/forgot-password?email=${encodeURIComponent(email)}`;
    return this.http.post<any>(url, {}).pipe(
      catchError(error => {
        throw error; // Throw the error directly
      })
    );
  }

  verifyOTP(email: string, otp: string): Observable<any> {
    const url = `http://localhost:5117/api/User/verify-otp?email=${encodeURIComponent(email)}&otp=${otp}`;
    return this.http.post<any>(url, {}).pipe(
      catchError(this.handleError)
    );
  }
  updatePassword(model: ChangePasswordModel): Observable<any> {
    return this.http.post<any>("http://localhost:5117/api/User/reset-password", model).pipe(
      catchError(this.handleError)
    );
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
