import { Component } from '@angular/core';
import { AuthService } from 'src/Service/auth.service';
import { OrderService } from 'src/Service/order.service';

@Component({
  selector: 'app-view-order',
  templateUrl: './view-order.component.html',
  styleUrls: ['./view-order.component.css']
})
export class ViewOrderComponent {
  orders: any[] =[]; // Define a property to hold the fetched orders

  constructor(private order:OrderService) { }

  ngOnInit(): void {
    this.loadOrders();
  }

  loadOrders() {

    this.order.getOrders()
      .subscribe((orders: any[]) => {
        this.orders = orders; 
      });
  }

}
