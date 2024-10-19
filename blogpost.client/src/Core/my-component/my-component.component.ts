import { Component ,OnInit} from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { AuthServiceService } from '../Services/auth-service.service';

@Component({
  selector: 'app-my-component',
  templateUrl: './my-component.component.html',
  styleUrl: './my-component.component.css'
})
export class MyComponentComponent implements OnInit{


  showNavbar: boolean = true;
isUserValid: boolean = false;  
logoutMessage: string = '';    

constructor(private router: Router, private authService: AuthServiceService) {

  this.router.events.subscribe((event) => {
    if (event instanceof NavigationEnd) {
     
      this.showNavbar = !(event.url === '/login' || event.url === '/singup');
    }
  });
}



ngOnInit() {
  this.checkLoginStatus();
}


checkLoginStatus() {
  const token = localStorage.getItem('authToken');
  if (token) {
    this.isUserValid = true;  
  } else {
    this.isUserValid = false; 
  }
}


logout() {
  debugger;
 
  localStorage.removeItem('authToken'); 
  
 
  this.isUserValid = false;             
  

  this.router.navigate(['/home']);
  

  this.logoutMessage = 'You have been logged out successfully!';
  

  setTimeout(() => {
    this.logoutMessage = '';
  }, 3000); 
  window.location.reload();
  
  console.log('User logged out');
}

}
