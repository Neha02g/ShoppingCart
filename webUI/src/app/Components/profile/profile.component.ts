// profile.component.ts

import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/Service/auth.service';
import { ProfileDataModel } from 'src/Models/profileDataModel';
import { Router } from '@angular/router';
import { ProfileService } from 'src/Service/profile.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {
  profileData: ProfileDataModel = {
    firstName: '',
    lastName: '',
    phoneNumber: ''
  };
  errorMessage: string = '';
  isEditing: boolean = false;
  successMessage:string='';


  constructor(private authService: ProfileService, private router: Router) {
    this.loadProfileData();
  }

  loadProfileData() {
    this.authService.getProfileData().subscribe({
      next: (response: any) => {
        this.profileData = response;
      },
      error: (error: any) => {
        this.errorMessage = 'An error occurred while fetching profile data.';
      }
    });
  }

  updateProfile() {

  this.authService.updateProfile(this.profileData).subscribe({
    next:() => {
      this.successMessage = 'Profile updated successfully';
      setTimeout(() => {
        this.successMessage = '';
        this.router.navigate(['/']); // Navigate to the home page (AppComponent)
      }, 3000); // Display the message for 3 seconds
    },
    error:(error: any) => {
      console.error('Error:', error);
      this.errorMessage = error.error;
      setTimeout(() => {
        this.errorMessage = '';
      }, 2000); // Display the message for 3 seconds
    }
  });
  }
enableEditing() {
  this.isEditing = true;
}

cancelEditing() {
  this.isEditing = false;
}

}


//   updateProfile() {
//     this.authService.updateProfile(this.profileData).subscribe(
//       () => {
//         // Handle successful profile update
//         this.router.navigate(['']); // Redirect to dashboard or any other page
//       },
//       (error: any) => {
//         console.error('Error:', error);
//         this.errorMessage = error.error; // Display the error message from the server
//       }
//     );
// }

