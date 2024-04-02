import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { ProfileDataModel } from 'src/Models/profileDataModel';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  constructor(private http:HttpClient) { }
  updateProfile(profile: ProfileDataModel): Observable<any> {
    // Retrieve the authentication token from localStorage
    const authToken = localStorage.getItem('token');

    // Set the authorization header with the token
    const headers = new HttpHeaders().set('Authorization', `Bearer ${authToken}`);

    // Make the HTTP request with the token included in the headers
    return this.http.post<any>("http://localhost:5117/api/Profile/UpdateProfile", profile, { headers });
  }

  getProfileData(): Observable<ProfileDataModel> {
    // Get the token from local storage
    const token = localStorage.getItem('token');

    // Create headers with authorization token
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    // Make the request with headers
    return this.http.get<ProfileDataModel>("http://localhost:5117/api/Profile/GetProfileData", { headers: headers })
      .pipe(
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
