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
 

constructor(private singupService:SingupService,  private router: Router, private cd: ChangeDetectorRef )
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







isSubmitting = false;

registerFormSubmit(form: NgForm) {
  this.passwordMismatch = this.formData.pwd !== this.formData.rpwd;
  
  if (form.valid && !this.passwordMismatch) {
    this.isSubmitting = true;
    this.cd.detectChanges();  

    const { rpwd, ...userToSave } = this.formData;  
    
    this.singupService.Singup(userToSave).subscribe(
      response => {
        console.warn("Success:", response);
        this.successMessageVisible = true;
        this.isSubmitting = false;
        this.cd.detectChanges();  
      
        this.router.navigate(['/login']);

        setTimeout(() => {
          this.successMessageVisible = false;
          this.cd.detectChanges();  
          
        }, 3000);
      },
      error => {
        this.isSubmitting = false;
        console.error('Error registering user', error);
        this.cd.detectChanges();  
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
