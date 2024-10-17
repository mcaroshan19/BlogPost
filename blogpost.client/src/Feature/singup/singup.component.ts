import { Component , OnInit } from '@angular/core';
import { NgForm  } from '@angular/forms';
import { SingupService } from '../../Core/Services/singup.service';
import { Router } from '@angular/router';
import { ChangeDetectorRef } from '@angular/core'; 



import { SingupUser } from '../../Core/Model/SingupUser';
@Component({
  selector: 'app-singup',
  templateUrl: './singup.component.html',
  styleUrl: './singup.component.css'
})
export class SingupComponent  implements OnInit  {
  ngOnInit(): void {}
 

constructor(private singupService:SingupService, private router: Router, private cd: ChangeDetectorRef )
{

}



successMessageVisible= false;
passwordFieldType: string = 'password';
confirmPasswordFieldType: string = 'password';
passwordIcon: string = 'fas fa-eye'; 

confirmPasswordIcon: string = 'fa fa-eye'; 
passwordMismatch: boolean = false;



  

formData: SingupUser = {
  firstname: '',
  lastname: '',
  email: '',
  Mobile: '',
  gender: '',
  pwd: '',
  rpwd: ''
};





// registerFormSubmit(form: NgForm) {
//   debugger
//   this.passwordMismatch = this.formData.pwd !== this.formData.rpwd;

//   if (form.valid && !this.passwordMismatch) {
    
//     const { rpwd, ...userToSave } = this.formData; 

//     this.singupService.Singup(userToSave).subscribe(
//       (response) => {
//         console.warn('User registered successfully!', response);
//         this.successMessageVisible = true;
        

//         setTimeout(() => {
//           this.successMessageVisible = false;
//           this.router.navigate(['/login']);
         
//         }, 3000);
//       },
//       error=> {
//         console.error('Error registering user', error);
//       }
//     );

//     form.reset(); 
//     this.formData = { firstname: '',  lastname: '', email: '',
//     Mobile: '',
//     gender: '',
//     pwd: '',
//     rpwd: '' }; 
//   }
// }


// registerFormSubmit(form: NgForm) {
//   this.passwordMismatch = this.formData.pwd !== this.formData.rpwd;

//   if (form.valid && !this.passwordMismatch) {
//     const { rpwd, ...userToSave } = this.formData; // Exclude repeated password for saving

//     this.singupService.Singup(userToSave).subscribe(
//       (response) => {
//         console.warn('User registered successfully!', response);
//         this.successMessageVisible = true;

//         setTimeout(() => {
//           this.successMessageVisible = false;
//           this.router.navigate(['/login']); // Navigate to login page
//         }, 3000);
//       },
//       error => {
//         console.error('Error registering user', error);
//       }
//     );

//     form.reset(); 
//     this.formData = { 
//       firstname: '',  
//       lastname: '', 
//       email: '',
//       Mobile: '',
//       gender: '',
//       pwd: '',
//       rpwd: '' 
//     };
//   }
// }




// registerFormSubmit(form: NgForm) {
//   this.passwordMismatch = this.formData.pwd !== this.formData.rpwd;

//   if (form.valid && !this.passwordMismatch) {
//     const { rpwd, ...userToSave } = this.formData; // Exclude repeated password for saving

//     this.singupService.Singup(userToSave).subscribe(
//       (response) => {
//         console.warn('User registered successfully!', response);
//         this.successMessageVisible = true;

//         setTimeout(() => {
//           this.successMessageVisible = false;
//           this.router.navigate(['/login']); // Navigate to login page
//         }, 3000);
//       },
//       error => {
//         console.error('Error registering user', error);
//       }
//     );

//     form.reset(); 
//     this.formData = { 
//       firstname: '',  
//       lastname: '', 
//       email: '',
//       Mobile: '',
//       gender: '',
//       pwd: '',
//       rpwd: '' 
//     };
//   }
// }


isSubmitting = false;

registerFormSubmit(form: NgForm) {
  this.passwordMismatch = this.formData.pwd !== this.formData.rpwd;
  
  if (form.valid && !this.passwordMismatch) {
    this.isSubmitting = true;
    this.cd.detectChanges();  // Manually trigger change detection

    const { rpwd, ...userToSave } = this.formData;  // Remove repeated password
    
    this.singupService.Singup(userToSave).subscribe(
      (response) => {
        this.successMessageVisible = true;
        this.isSubmitting = false;
        this.cd.detectChanges();  // Trigger detection after success message

        setTimeout(() => {
          this.successMessageVisible = false;
          this.cd.detectChanges();  // Update UI before navigation
          this.router.navigate(['/home']);  // Ensure /home is correct in the router
        }, 3000);
      },
      error => {
        this.isSubmitting = false;
        console.error('Error registering user', error);
        this.cd.detectChanges();  // Trigger detection on error
      }
    );

    form.reset();
    this.formData = {
      firstname: '',
      lastname: '',
      email: '',
      Mobile: '',
      gender: '',
      pwd: '',
      rpwd: ''
    };
  }
}

}
