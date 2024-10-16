import { Component, OnInit } from '@angular/core';
import { NgForm  } from '@angular/forms';
import { SingupService } from '../../Core/Services/singup.service';
import { Router } from '@angular/router';



import { SingupUser } from '../../Core/Model/SingupUser';
@Component({
  selector: 'app-singup',
  templateUrl: './singup.component.html',
  styleUrl: './singup.component.css'
})
export class SingupComponent    implements OnInit{
 
ngOnInit(): void {
    
}
constructor(private singupService:SingupService, private router: Router)
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





registerFormSubmit(form: NgForm) {
  debugger
  this.passwordMismatch = this.formData.pwd !== this.formData.rpwd;

  if (form.valid && !this.passwordMismatch) {
    
    const { rpwd, ...userToSave } = this.formData; 

    this.singupService.Singup(userToSave).subscribe(
      (response) => {
        console.warn('User registered successfully!', response);
        this.successMessageVisible = true;
        

        setTimeout(() => {
          this.successMessageVisible = false;
          this.router.navigate(['/login']);
         
        }, 3000);
      },
      error=> {
        console.error('Error registering user', error);
      }
    );

    form.reset(); 
    this.formData = { firstname: '',  lastname: '', email: '',
    Mobile: '',
    gender: '',
    pwd: '',
    rpwd: '' }; 
  }
}
}
