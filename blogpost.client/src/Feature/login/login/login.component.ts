import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { loginModel } from '../../../Core/Model/loginModel';
import { SingupService } from '../../../Core/Services/singup.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

//   UserLogin: loginModel = {
//     Email: '',
//     Pwd: ''
//   };

//   constructor( private userlogin:SingupService ,private router: Router) {}

//   loginWithFacebook() {

//   }
//   onSubmit(form: NgForm) {
//     debugger; 
//     if (form.valid) {
//       this.userlogin.Login(form.value).subscribe(
//         (res) => {
//           console.log("Login successful.", res);
//         },
//         (error) => {
//           console.error("Login failed.", error);
//         }
//       );
//     } else {
//       console.log('Form is invalid');
//     }
  
//   }

// }



 
  // Define model for two-way binding
  UserLogin: loginModel = {
    Email: '',
    Pwd: ''
  };

  // Properties for handling UI state
  isUserValid: boolean = false;
  loginError: string | null = null;

  constructor(private userlogin: SingupService, private router: Router) {}

  // Method to handle Facebook login
  loginWithFacebook() {
    console.log('Facebook login logic will go here.');
  }

  // Form submission logic
  onSubmit(form: NgForm) {
    // Check if the form is valid before submitting
    if (form.valid) {
      this.userlogin.Login(form.value).subscribe(
        (res: any) => {
          if (res.token) {
            this.isUserValid = true;
            this.loginError = null;
            console.log('Login Successful', res);
            
            // Save token and navigate to a different route
            localStorage.setItem('authToken', res.token);
            this.router.navigate(['/home']);
          } else {
            this.isUserValid = false;
            this.loginError = 'Login Unsuccessful. Please check your credentials.';
            console.log('Login Unsuccessful');
          }
        },
        (error) => {
          console.error('Login request failed', error);
          this.loginError = 'An error occurred. Please try again.';
        }
      );
    } else {
      console.warn('Form is invalid');
      this.loginError = 'Please fill in all fields.';
    }
  }
}







