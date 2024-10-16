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




 
  
  UserLogin: loginModel = {
    Email: '',
    Pwd: ''
  };

  
  isUserValid: boolean = false;
  loginError: string | null = null;

  constructor(private userlogin: SingupService, private router: Router) {}


  loginWithFacebook() {
    console.log('Facebook login logic will go here.');
  }

  
  onSubmit(form: NgForm) {
    
    if (form.valid) {
      this.userlogin.Login(form.value).subscribe(
        (res: any) => {
          if (res.token) {
            this.isUserValid = true;
            this.loginError = null;
            console.log('Login Successful', res);
            
           
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







