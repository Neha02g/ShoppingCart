import { Component } from '@angular/core';
import { ChangePasswordModel } from 'src/Models/changePasswordModel';
import { AuthService } from 'src/Service/auth.service';
import { ForgotPasswordService } from 'src/Service/forgot-password.service';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent {
email:string='';
emailForReset:string='';
otp:string='';
success:string='';
errormsg:string='';

passwordChanged:boolean = false;
otpSent:boolean = false;

changePasswordData: ChangePasswordModel = {
  Email: '',
  NewPassword: '',
  ConfirmPassword: ''
};
constructor(private service:ForgotPasswordService,private route:Router){}

sendOTP() {
  if (this.email) {
    this.service.sendOTP(this.email).subscribe(
     (response:any) => {
        this.success = "OTP sent successfully";
        this.otpSent = true;
        setTimeout(() => {
          this.success = ''; // Clear the error message after 5 seconds
        }, 1000);

        // Handle success, show a message or redirect to another page
      }
  );
  } else {
    console.error('Email is required');
    this.errormsg="Email is required";
    setTimeout(() => {
      this.errormsg = '';
    }, 1000);

    // Handle error, show an error message to the user
  }
}
verifyOTP() {
  if (this.emailForReset && this.otp) {
    this.service.verifyOTP(this.emailForReset, this.otp).subscribe(
      {next:(response:any) => {
        this.passwordChanged = true;
        // Handle success, show a success message or redirect to another page
      },
      error:(error:any) => {
        this.errormsg = "Invalid email or otp";
        setTimeout(() => {
          this.errormsg = '';
        }, 1000);

        // Handle error, show an error message to the user
      }
  });
  } else {
    this.errormsg = "Email and otp are required";
    setTimeout(() => {
      this.errormsg = '';
    }, 1000);
    // Handle error, show an error message to the user
  }
}
changePassword() {
  if (this.changePasswordData.Email && this.changePasswordData.Email==this.emailForReset &&this.changePasswordData.NewPassword === this.changePasswordData.ConfirmPassword) {
    this.service.updatePassword(this.changePasswordData).subscribe({
     next: () => {
        this.success="Password Reset SuccessFully";
        setTimeout(() => {
          this.success = '';
          this.route.navigate(['/home']);
        }, 1000);
      },
      error:(error) => {
        this.errormsg = 'An error occurred while changing password.';
        setTimeout(() => {
          this.errormsg = '';
        }, 1000);
        // Handle error, show an error message to the user
      }
  });
  } else {
    this.errormsg ="Email is required and passwords must match";
    setTimeout(() => {
      this.errormsg = ''; 
    }, 1000);
    // Handle error, show an error message to the user
  }
}
}
