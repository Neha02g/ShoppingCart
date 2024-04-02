import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/Service/auth.service';
import { CartService } from 'src/Service/cart.service';
import { PaymentService } from 'src/Service/payment.service';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.css']
})
export class TransactionComponent {
  selectedPaymentMethod: string = 'card';
  form = new FormGroup({
    cardNumber: new FormControl('', [Validators.required, Validators.pattern('[0-9]{16}')]),
    expirationDate: new FormControl('', [Validators.required, Validators.pattern('^(0[1-9]|1[0-2])\/?([0-9]{2})$')]),
    cvv: new FormControl('', [Validators.required, Validators.pattern('[0-9]{3,4}')]),
    nameOnCard: new FormControl('', [Validators.required,Validators.pattern('^[A-Z a-z]{2,}$')])
  });

  constructor(private service:PaymentService,private route:Router,private auth:CartService)
  {

  }

  selectPaymentMethod(method: string): void {
    this.selectedPaymentMethod = method;
  }

  completePayment(){
    if (this.selectedPaymentMethod) {
      this.service.Payment(this.selectedPaymentMethod).subscribe(
       (response:any) => {
       // this.authService.getCartItems();
          console.log('Order placed successfully', response);
          this.auth.getCartItems();
         this.route.navigate(['/confirmOrder']);
        
        }
    );
    } else {
      console.error('Something went wrong');
      // Handle error, show an error message to the user
    }
  }
}
