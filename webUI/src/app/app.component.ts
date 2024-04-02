import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/Service/auth.service';
import { CartService } from 'src/Service/cart.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'EasyShop';
  isLoggedIn: boolean | undefined;
  userEmail: string | null=null;
  cartValue:number=0;
  cartItems:any;


  constructor(private authService: AuthService,private cart:CartService ,private router:Router) { }

  ngOnInit(): void {
    this.isLoggedIn = this.authService.isLoggedIn();
    this.userEmail = this.authService.getUserEmail();

    // Subscribe to the authChanged event emitter
    this.authService.authChanged.subscribe((isLoggedIn: boolean) => {
      this.isLoggedIn = isLoggedIn;
      if (isLoggedIn) {
        this.userEmail = this.authService.getUserEmail(); // Update user email when logged in
        this.updateCart(); // Update cart when logged in
      } 
    });
   // this.updateCart();

    
    this.cart.getCartItems().subscribe({
      next:(data: any) => {
        if (data && data.length > 0) {
          this.cart.cartItems$.subscribe({
            next:(data: any) => {
              this.cartValue=data;
            }});
          this.cartItems = data;

        } else {
          this.cartValue = 0;
          this.cartItems = null;
        }
      },
      error:(error: any) => {
        console.error('Error fetching cart items:', error);

      }
    });
  }
  stringifyCartItems(): string {
    return JSON.stringify(this.cartItems);
  }

  updateCart() {
    this.cart.getCartItems().subscribe({
      next:(data: any) => {
        if (data && data.length > 0) {
          this.cart.cartItems$.subscribe({
            next:(data: any) => {
              this.cartValue=data;
            }});
          this.cartItems = data;

        } else {
          this.cartValue = 0;
          this.cartItems = null;
        }
      },
      error:(error: any) => {
        console.error('Error fetching cart items:', error);

      }
    });
  }



  logout()
  {
    this.authService.logout();
    this.cartValue=0;
    this.cartItems=null;
  }

  searchProducts(query: string) {
    // Navigate to the search route with the query parameter
    this.router.navigate(['/search'], { queryParams: { q: query } });
  }
}

