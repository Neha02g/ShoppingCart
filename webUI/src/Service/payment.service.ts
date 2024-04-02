import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, switchMap, tap, throwError } from 'rxjs';
import { CartService } from './cart.service';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  constructor(private http:HttpClient,private cart:CartService) { }

  
Payment(PaymentMethod: string) {
  const token = localStorage.getItem('token');
  console.log(PaymentMethod);
  const url = `http://localhost:5117/api/Transaction/PaymentMethod?PaymentMethod=${encodeURIComponent(PaymentMethod)}`;

  // Set up headers with the token
  const headers = new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${token}`
  });

  return this.http.post<any>(url, {}, { headers: headers }).pipe(
    tap(()=>{
         this.cart.getCartItems();
    }),
    catchError(error => {
      return throwError(()=>new Error(error)); // Throw the error directly
    })
  );
}
}
