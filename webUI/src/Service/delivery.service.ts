import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { DeliveryDetailModel } from 'src/Models/deliveryDetailModel';

@Injectable({
  providedIn: 'root'
})
export class DeliveryService {

  constructor(private http:HttpClient) { }

  
addDeliveryDetails(data: DeliveryDetailModel): Observable<any> {
  const token = localStorage.getItem('token');
  const headers = new HttpHeaders({
    'Authorization': `Bearer ${token}`,
    'Content-Type': 'application/json'
  });

  const url = `http://localhost:5117/api/Delivery/AddDeliveryDetails`;
  return this.http.post<any>(url, data, { headers }).pipe(
    catchError((error: HttpErrorResponse) => {
      console.error('Error occurred:', error);
      return throwError(() => new Error(error.message));
    })
  );
}

}
