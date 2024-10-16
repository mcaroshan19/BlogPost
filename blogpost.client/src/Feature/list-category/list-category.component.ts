import { Component,OnInit } from '@angular/core';


import { ResponseModel } from '../../Core/Model/ResponseModel';
import { PostDataService } from '../../Core/Services/post-data.service';
import { NgForm } from '@angular/forms';

import * as XLSX from 'xlsx'; 

@Component({
  selector: 'app-list-category',
  templateUrl: './list-category.component.html',
  styleUrl: './list-category.component.css'
})
export class ListCategoryComponent implements OnInit{




userData: ResponseModel[] = [];
searchQuery = '';
isPopupVisible = false;
successMessageVisible = false;
editingCategoryId: number | null = null;
categoryFormData: { name: string; urlHandle: string } = { name: '', urlHandle: '' };

constructor(private postDataService: PostDataService) {}


ngOnInit(): void {
  this.GetBlogpostData();
}

// Fetch data from the API
GetBlogpostData(): void {
  this.postDataService.getData().subscribe(data => {
    console.log('Data received from API:', data);
    this.userData = data;
    console.log('Assigned userData:', this.userData);
  }, error => {
    console.error('Error fetching data', error);
  });
}


openPopup(category?: ResponseModel): void {

  this.isPopupVisible = true;
  this.editingCategoryId = category?.id ?? null;

  if (category) {
    // Editing existing category
    this.categoryFormData = { name: category.name, urlHandle: category.urlHandle };
  } else {
    // Adding new category
    this.categoryFormData = { name: '', urlHandle: '' };
  }
}


closePopup(): void {
  this.isPopupVisible = false;
  this.editingCategoryId = null;
}

onFormSubmit(usersForm: NgForm): void {
  if (usersForm.valid) {
    if (this.editingCategoryId !== null) {
      // Editing existing category
      this.postDataService.updateCategory(this.editingCategoryId, usersForm.value).subscribe(result => {
        console.warn("Success:", result);
        this.successMessageVisible = true;
        setTimeout(() => {
          this.successMessageVisible = false;
          this.GetBlogpostData(); // Refresh the list after update
          this.closePopup();
        }, 3000);
      }, error => {
        console.error('Error occurred:', error);
      });
    } else {
      // Adding new category
      this.postDataService.addCategory(usersForm.value).subscribe(result => {
        console.warn("Success:", result);
        this.successMessageVisible = true;
        setTimeout(() => {
          this.successMessageVisible = false;
          this.GetBlogpostData(); // Refresh the list after adding
          this.closePopup();
        }, 3000);
      }, error => {
        console.error('Error occurred:', error);
      });
    }

    usersForm.reset();
  }
}
 // Export data to Excel
 exportToExcel(): void {
  // Convert data to a worksheet
  const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet(this.userData);

  // Create a new workbook and append the worksheet
  const wb: XLSX.WorkBook = XLSX.utils.book_new();
  XLSX.utils.book_append_sheet(wb, ws, 'Categories');

  // Generate buffer and save file
  XLSX.writeFile(wb, 'categories.xlsx');
}
deleteCategory(id: number): void {
  if (confirm("Are you sure you want to delete this category?")) {
    this.postDataService.deleteCategory(id).subscribe(result => {
      console.warn("Category deleted successfully:", result);
      this.GetBlogpostData(); // Refresh the list after deletion
    }, error => {
      console.error('Error occurred while deleting:', error);
    });
  }
}


}




 


