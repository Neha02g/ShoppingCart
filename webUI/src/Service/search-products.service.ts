import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SearchProductsService {

  constructor(private http:HttpClient) { }

  searchProducts(query: string): Observable<any> {
    return this.http.get(`http://localhost:5117/api/Products/searchBy?query=${query}`).pipe(
      catchError(error => {
        throw error; // Throw the error directly
      })
    );
  }
  
  getAllProducts(): Observable<any> {
    return this.http.get("http://localhost:5117/api/Products/search").pipe(
      catchError(error => {
        throw error; // Throw the error directly
      })
    );
  }
}
