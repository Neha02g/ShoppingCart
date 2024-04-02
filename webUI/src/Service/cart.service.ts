import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, catchError, tap, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  private cartItemsSubject: BehaviorSubject<number> = new BehaviorSubject<number>(0);
  public cartItems$: Observable<number> = this.cartItemsSubject.asObservable();
 
  constructor(private http:HttpClient) { }

  
getCartItems(): Observable<any> {
  // Check if the user is logged in and has a token
  const token = localStorage.getItem('token');
  if (!token) {
    // Handle the case where the user is not logged in
    return throwError(() => new Error('User is not logged in'));
  }

  // Construct the request headers with the JWT token
  const headers = new HttpHeaders({
    'Authorization': `Bearer ${token}`
  });

  // Make the HTTP request with the headers
  return this.http.get<any[]>("http://localhost:5117/api/Cart/ViewCart", { headers }).pipe(
    tap(cartItems => {

      let totalQuantity = 0;
      cartItems.forEach(item => {
        totalQuantity += item.quantity;
      });
      // Update the BehaviorSubject with the total quantity
      this.cartItemsSubject.next(totalQuantity);
    }),
    catchError(error => {
      throw error; // Re-throw the error to be caught by the caller
    })
  );
}



getCartSummary(): Observable<any> {

  const token = localStorage.getItem('token');
  // Add authentication headers if needed
  const headers = new HttpHeaders({
    'Authorization': `Bearer ${token}` // Replace 'your_access_token' with the actual access token
  });

  return this.http.get<any>("http://localhost:5117/api/Cart/CartSummary", { headers });
}

addToCart(id: number): Observable<any> {
  const token = localStorage.getItem('token');
  const headers = new HttpHeaders({
    'Authorization': `Bearer ${token}`
  });

  const url = `http://localhost:5117/api/Cart/AddToCart/${id}`;
  return this.http.post<any>(url, {}, { headers })
  .pipe(
    tap(() => {
      //Increment cart item count when an item is added
      //this.getCartItems();
      this.cartItemsSubject.next(this.cartItemsSubject.value + 1);
    })
  );
}

RemoveFromCart(id: number): Observable<any> {
  const token = localStorage.getItem('token');
  const headers = new HttpHeaders({
    'Authorization': `Bearer ${token}`
  });

  const url = `http://localhost:5117/api/Cart/RemoveFromCart/${id}`;
  return this.http.post<any>(url, {}, { headers }).pipe(
    tap(() => {
      // Decrement cart item count when an item is removed
      // const currentValue = this.cartItemsSubject.value;
      // this.cartItemsSubject.next(Math.max(0, currentValue - 1));
      this.cartItemsSubject.next(this.cartItemsSubject.value-1);
    })
  );
}


}
