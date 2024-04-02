import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { error } from 'jquery';
import { AuthService } from 'src/Service/auth.service';
import { CartService } from 'src/Service/cart.service';
import { OrderService } from 'src/Service/order.service';

@Component({
  selector: 'app-cart-items',
  templateUrl: './cart-items.component.html',
  styleUrls: ['./cart-items.component.css']
})
export class CartItemsComponent {
  cartItems: any;
  cartSummary:any;

  constructor(private route: Router,private cart:CartService,private orders:OrderService) { }

  ngOnInit(): void {
    // Get the cart items string from query parameters
    this.cart.getCartItems().subscribe({
      next:(data: any) => {
        this.cartItems = data;
      },
      error:(error: any) => {
        console.error('Error fetching cart summary:', error);
      }
     });


    // Fetch the cart summary
    this.cart.getCartSummary().subscribe({
      next:(summary: any) => {
        this.cartSummary = summary;
      },
      error:(error: any) => {
        console.error('Error fetching cart summary:', error);
      }
     });
  }

  updateQuantity(id: number) {

    // Call the service method to update the quantity
    this.cart.addToCart(id).subscribe(() => {
      // Update cart items after adding to cart
      this.cart.getCartItems().subscribe((items: any[]) => {
        this.cartItems = items;
      });

      // Update cart summary after adding to cart
      this.cart.getCartSummary().subscribe((summary: any) => {
        this.cartSummary = summary;
      });
    });
  }



  decreseQuantity(id: number) {

    // Call the service method to update the quantity
    this.cart.RemoveFromCart(id).subscribe(() => {
      // Update cart items after adding to cart
      this.cart.getCartItems().subscribe({
        next:(items: any[]) => {
        this.cartItems = items;
        },
        error:(error:any)=>
        {
           this.cartItems=null;
           
        }
      });

      // Update cart summary after adding to cart
      this.cart.getCartSummary().subscribe((summary: any) => {
        this.cartSummary = summary;
      });
    });
  }

  order()
  {
    this.orders.Order().subscribe(() => {
      this.route.navigate(['/delivery-details']);
    });

  }
}
