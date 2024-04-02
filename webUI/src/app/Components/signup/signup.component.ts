import { Component } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterModel } from 'src/Models/register';
import { AuthService } from 'src/Service/auth.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {
  passerror='';

  signUpForm =new FormGroup({
    firstName: new FormControl('',[Validators.pattern('^[A-Za-z]{2,}$')]),
    lastName: new FormControl('',[Validators.pattern('^[A-Za-z]{2,}$')]),
    email: new FormControl('',[Validators.pattern('^(?=.{1,256}$)[a-zA-Z0-9_-]+(?:\.[a-zA-Z0-9_-]+)*@gmail\.com$')]),
    phoneNumber: new FormControl('',[Validators.pattern('^[0-9]{10}$')]),
    password: new FormControl('', Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^A-Za-z0-9]).{6,}$')),
    confirmPassword: new FormControl('',Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^A-Za-z0-9]).{6,}$'))
  });

  errormsg: string = '';

  constructor(private signupService: AuthService, private router: Router) {}

  submitForm() {

    this.signupService.signup(this.signUpForm.value).subscribe({
      next:response => {
        // Handle successful response (e.g., navigate to another page)
        this.router.navigate(['login']);
      },
      error:error => {
        // Handle error (e.g., display error message)
        console.error('Error:', error);
          this.errormsg = error;
      }
  });
  }

  get firstName()
  {
    return this.signUpForm.get('firstName');
  }
  get lastName()
  {
    return this.signUpForm.get('lastName');
  }
  get email(){
    return this.signUpForm.get('email');
  }
  get phoneNumber():FormControl{
    return this.signUpForm.get('phoneNumber') as FormControl;
  }
  get password():FormControl
  {
    return this.signUpForm.get('password') as FormControl;
  }
  get confirmPassword()
  {
    return this.signUpForm.get('confirmPassword');
  }
}

