import { Component } from '@angular/core';
import { FormControl, FormGroup , Validators} from '@angular/forms';
import { Router } from '@angular/router';
import { DeliveryDetailModel } from 'src/Models/deliveryDetailModel';
import { AuthService } from 'src/Service/auth.service';
import { DeliveryService } from 'src/Service/delivery.service';

@Component({
  selector: 'app-delivery-details',
  templateUrl: './delivery-details.component.html',
  styleUrls: ['./delivery-details.component.css']
})
export class DeliveryDetailsComponent {
constructor(private delivery :DeliveryService,private router:Router){}

  deliveryDetails: DeliveryDetailModel = {
    FullName: '',
    Address: '',
    Pincode: '',
    MobileNumber: ''
  };


  getfromForm = new FormGroup(
    {
      fname: new FormControl('',[Validators.pattern('^[A-Za-z]{2,}$')]),
    lname:new FormControl('',[Validators.pattern('^[A-Za-z]{2,}$')]),
    address:new FormControl('',[Validators.pattern('^[A-Z a-z ,. 0-9]{2,}$')]),
    phonenumber:new FormControl('',[Validators.pattern('^[0-9]{10}$')]),
    city:new FormControl('',[Validators.pattern('^[A-Za-z]{3,}$')]),
    state:new FormControl('',[Validators.pattern('^[A-Z a-z]{2,}$')]),
    zipcode:new FormControl('',[Validators.pattern('^[0-9]{6}$')])
    }
  )

  onSubmit()
  {
    this.deliveryDetails.FullName = this.getfromForm.get('fname')?.value+" "+this.getfromForm.get('lname')?.value;
    this.deliveryDetails.Address = this.getfromForm.get('address')?.value+" "+this.getfromForm.get('city')?.value+" "+this.getfromForm.get('state')?.value;
    this.deliveryDetails.Pincode = this.getfromForm.get('zipcode')?.value as string;
    this.deliveryDetails.MobileNumber = this.getfromForm.get('phonenumber')?.value as string;


    this.delivery.addDeliveryDetails(this.deliveryDetails).subscribe(() => {
      this.router.navigate(['/transaction']);
    });
  }

  get fname(){
    return this.getfromForm.get('fname');
  }
  get lname(){
    return this.getfromForm.get('lname');
  }
  get address(){
    return this.getfromForm.get('address');
  }
  get city(){
    return this.getfromForm.get('city');
  }
  get phonenumber(){
    return this.getfromForm.get('phonenumber');
  }
  get state(){
    return this.getfromForm.get('state');
  }
  get zipcode(){
    return this.getfromForm.get('zipcode');
  }
}
