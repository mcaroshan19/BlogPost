import { Component ,OnInit} from '@angular/core';
import { Router } from '@angular/router';
import { AuthServiceService } from '../Services/auth-service.service';

@Component({
  selector: 'app-my-component',
  templateUrl: './my-component.component.html',
  styleUrl: './my-component.component.css'
})
export class MyComponentComponent implements OnInit{

//   constructor(private router: Router,  private authService:AuthServiceService){}


//   isUserValid: boolean = false;
//   ngOnInit() {
//     const token = localStorage.getItem('authToken');
//     if (token) {
//       this.isUserValid = true;  // User is already logged in
//     }
//   }

//   // logout() {
//   //   debugger

//   //   localStorage.removeItem('authToken');

//   //   this.router.navigate(['/home']);
//   //   console.log('User logged out');
//   // }


//   logoutMessage: string = '';

//   logout() {
//     debugger;
//     localStorage.removeItem('authToken'); 
//     this.isUserValid = false;             
  
//     this.router.navigate(['/home']);       
    
    
//     this.logoutMessage = 'You have been logged out successfully!';
    
  
//     setTimeout(() => {
//       this.logoutMessage = '';
//     }, 3000); 
  
//     console.log('User logged out');
//   }


// }





isUserValid: boolean = false;  // Track user login status
logoutMessage: string = '';    // Message displayed after logout

constructor(private router: Router, private authService: AuthServiceService) {}

ngOnInit() {
  this.checkLoginStatus();
}

// Method to check if user is logged in based on token
checkLoginStatus() {
  const token = localStorage.getItem('authToken');
  if (token) {
    this.isUserValid = true;  // User is logged in
  } else {
    this.isUserValid = false; // User is logged out
  }
}

// Logout method to remove token, update login status, and show logout message
logout() {
  debugger;
  // Remove token from localStorage
  localStorage.removeItem('authToken'); 
  
  // Set user status to logged out
  this.isUserValid = false;             
  
  // Redirect to home page after logout
  this.router.navigate(['/home']);
  
  // Display logout success message
  this.logoutMessage = 'You have been logged out successfully!';
  
  // Hide the message after 3 seconds
  setTimeout(() => {
    this.logoutMessage = '';
  }, 3000); 
  window.location.reload();
  
  console.log('User logged out');
}

}
