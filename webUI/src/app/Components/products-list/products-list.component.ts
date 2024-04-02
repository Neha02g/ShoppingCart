import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/Service/auth.service';
import { CartService } from 'src/Service/cart.service';
import { SearchProductsService } from 'src/Service/search-products.service';

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css']
})
export class ProductsListComponent implements OnInit{
  products: any[] = [];
  showMessage: boolean = false;

  constructor(private cart: CartService, private search : SearchProductsService, private route: ActivatedRoute,private router:Router) {}
errormsg:any= '';

  ngOnInit(): void {
    // Get the query parameter from the route
    this.route.queryParams.subscribe({
      next: (params) => {
        const query = params['q'];
        if (query) {
          // Call the API to search for products
          this.search.searchProducts(query).subscribe({
            next: (data: any) => {
              this.products = data;
              console.warn(this.products);
            },
            error: (error: any) => {
              this.errormsg = error.error;
              setTimeout(() => {
                this.errormsg = null; // Clear the error message after 5 seconds
              }, 1000);
            }
          });
        } else {
          // If no query parameter is provided, load all products
          this.search.getAllProducts().subscribe({
            next: (data: any) => {
              this.products = data;
              console.warn(this.products);
            },
            error: (error: any) => {
              this.errormsg = error.error;
              setTimeout(() => {
                this.errormsg = null; // Clear the error message after 5 seconds
              }, 1000);
            }
          });
        }
      },
      error: (error) => {
        console.error('Error fetching query parameters:', error);
      }
    });
  }
  addtocart(id:number){
    this.cart.addToCart(id).subscribe({
      next:(response: any) => {
        // Add your add to cart logic here
        console.log(response);
        this.showMessage = true;
        setTimeout(() => {
          this.showMessage = false;
        }, 3000); // 3 seconds
        this.cart.getCartItems();
      },
      error:(error: any) => {
        this.router.navigate(['/login']);
      }
  });
  
    
  }

}
