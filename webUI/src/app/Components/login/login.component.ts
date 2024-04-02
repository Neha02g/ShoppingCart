import { Component, EventEmitter } from '@angular/core';
import { FormControl, FormGroup ,Validators} from '@angular/forms';
import { Route, Router } from '@angular/router';
import { AuthService } from 'src/Service/auth.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  authChanged: EventEmitter<boolean> = new EventEmitter<boolean>();
  errorMsg:string='';
loginForm = new FormGroup({
  Email:new FormControl('',[Validators.pattern('^(?=.{1,256}$)[a-zA-Z0-9_-]+(?:\.[a-zA-Z0-9_-]+)*@gmail\.com$')]),
  Password:new FormControl('',[Validators.minLength(6)]),
})
  constructor(private service:AuthService,private router:Router)
  {

  }

  // customerLogin()
  // {
  //      this.service.login(this.loginForm.value);

  // }

  customerLogin() {
    this.service.login(this.loginForm.value)
      .subscribe({
        next:(result: any) => {
          // Handle success response
          localStorage.setItem("token", result.token);
          localStorage.setItem("userEmail", this.loginForm.value.Email?this.loginForm.value.Email:'User');
          this.authChanged.emit(true);
          this.router.navigate(['']);
          this.service.setLoginData(result.token,this.loginForm.value.Email?this.loginForm.value.Email:'User');
        },
        error:(error: any) => {
          // Handle error response
          this.errorMsg = error; // Assign the error message to the errorMsg variable
        }
  });
  }

  get Email(){
    return this.loginForm.get('Email');
  }

  get Password(){
    return this.loginForm.get('Password');
  }
}
