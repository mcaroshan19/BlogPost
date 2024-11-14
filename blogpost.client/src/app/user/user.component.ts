import { Component, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../Core/Services/user.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  successMessage: string | null = null;
  fileTooLarge: boolean = false;
  fileUploaded: boolean = false;
  steps = ['Personal Details', 'Employee Details', 'Job Details', 'Bank Details', 'File Upload'];
  currentStep = 0;

  personalForm: FormGroup;
  employeeForm: FormGroup;
  jobForm: FormGroup;
  bankForm: FormGroup;
  fileForm: FormGroup;

  constructor(private fb: FormBuilder, private userService: UserService,  private router: Router, private route: ActivatedRoute) {
    this.personalForm = this.fb.group({
      fullName: ['', Validators.required],
      dob: ['', Validators.required]
    });

    this.employeeForm = this.fb.group({
      employeeCode: ['', Validators.required]
    });

    this.jobForm = this.fb.group({
      jobTitle: ['', Validators.required]
    });

    this.bankForm = this.fb.group({
      bankName: ['', Validators.required]
    });

    this.fileForm = this.fb.group({
      file: [null, Validators.required]
    });
  }

  goToStep(step: number) {
    this.currentStep = step;
  }

  nextStep() {
    if (this.isCurrentFormValid()) {
      this.currentStep = Math.min(this.currentStep + 1, this.steps.length - 1);
    }
  }

  isCurrentFormValid(): boolean {
    switch (this.currentStep) {
      case 0: return this.personalForm.valid;
      case 1: return this.employeeForm.valid;
      case 2: return this.jobForm.valid;
      case 3: return this.bankForm.valid;
      case 4: return this.fileForm.valid;
      default: return false;
    }
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;

    if (input.files && input.files[0]) {
      const file = input.files[0];

      if (file.size > 3 * 1024 * 1024) {
        this.fileTooLarge = true;
        this.fileUploaded = false;
        this.fileForm.reset();
      } else {
        this.fileTooLarge = false;
        this.fileForm.get('file')?.setValue(file);
        this.fileUploaded = true;
      }
    }
  }

  get allFormsValid(): boolean {
    return (
      this.personalForm.valid &&
      this.employeeForm.valid &&
      this.jobForm.valid &&
      this.bankForm.valid &&
      this.fileForm.valid &&
      this.fileUploaded &&
      !this.fileTooLarge
    );
  }

  
  submitForm() {
    debugger;
    if (this.allFormsValid) {
      const formData = new FormData();
      formData.append('fullName', this.personalForm.value.fullName);
      formData.append('dob', this.personalForm.value.dob);
      formData.append('employeeCode', this.employeeForm.value.employeeCode);
      formData.append('jobTitle', this.jobForm.value.jobTitle);
      formData.append('bankName', this.bankForm.value.bankName);
  
      if (this.fileForm.value.file) {
        formData.append('file', this.fileForm.value.file);
      } else {
        console.log('No file selected.');
        return; 
      }
  
      this.userService.saveUser(formData).subscribe({
        next: (response) => {
          console.log('Form submitted successfully!', response);
          this.resetForms();
          this.router.navigate(['/home'], { queryParams: { message: 'Form submitted successfully!' } });
        },
        error: (error) => {
          console.error('There was an error!', error);
     
        }
      });
    } else {
      console.log('Please complete all required fields before submitting.');
    }
  }
  
  resetForms() {
    this.personalForm.reset();
    this.employeeForm.reset();
    this.jobForm.reset();
    this.bankForm.reset();
    this.fileForm.reset();
    this.fileUploaded = false;
    this.fileTooLarge = false;
    this.currentStep = 0; 
  }


  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.successMessage = params['message'] || null;

      if(this.successMessage){
         setTimeout(()=>
         {
          this.successMessage= null;
         }, 4000
         );

      }
    });
  }
  

}
