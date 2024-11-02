import { Component, OnInit } from '@angular/core';
import { BlogpostService } from '../../../Core/Services/blogpost.service';

import { HttpErrorResponse } from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { BlogpostModel } from '../../../Core/Model/Blogpost-Model';
import { Block } from '@angular/compiler';
@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrl: './add-category.component.css'
})
export class AddCategoryComponent {
  successMessageVisible  = false;
  isPopupVisible  = false; // Renamed to follow naming convention
  blogpostFormData: {
    Title: string;
    ShortDescription: string;
    Content: string;
    UrlHandle: string;
    FeaturedImageUrl: string;
    PublishedDate: Date;
    Author: string;
    Ticket: string;
    IsVisible: boolean;
  } = {
    Title: '',
    ShortDescription: '',
    Content: '',
    UrlHandle: '',
    FeaturedImageUrl: '',
    PublishedDate: new Date(), // Initialize with the current date
    Author: '',
    Ticket:'',
    IsVisible: false // Default to false
  };

  constructor(private blogpostService: BlogpostService) {}

  openPopup(blog?: BlogpostModel): void {
    this.isPopupVisible = true;
    if (blog) {
      // Editing existing blog post
      this.blogpostFormData = {
        Title: blog.Title || '', // Default to empty string if undefined
        ShortDescription: blog.ShortDescription || '',
        Content: blog.Content || '',
        UrlHandle: blog.UrlHandle || '',
        FeaturedImageUrl: blog.FeaturedImageUrl || '', // Default to empty if undefined
        PublishedDate: blog.PublishedDate || new Date(), // Default to current date if undefined
        Author: blog.Author || '', // Default to empty if undefined
        Ticket:blog.Ticket || '',
        IsVisible: blog.IsVisible !== undefined ? blog.IsVisible : false // Default to false if undefined
      };
    } else {
      // Adding new blog post
      this.blogpostFormData = {
        Title: '',
        ShortDescription: '',
        Content: '',
        UrlHandle: '',
        FeaturedImageUrl: '',
        PublishedDate: new Date(), // Default to current date
        Author: '',
        Ticket:'',
        IsVisible: false // Default to false
      };
    }
  }

  closePopup(): void {
    this.isPopupVisible = false;
  }

  BlogpostSave(BlogForm: NgForm): void {
    debugger
    if (!BlogForm) {
      console.error('Form is undefined');
      return;
    }

    if (BlogForm.valid) {
      this.blogpostService.Postdata(BlogForm.value).subscribe(result => {
        console.warn("Success:", result);
        this.successMessageVisible = true;

        setTimeout(() => {
          this.successMessageVisible = false;
          this.closePopup();
        }, 3000);
      }, error => {
        console.error('Error occurred:', error);
      });

      // Reset form and formData
      BlogForm.resetForm();
      this.blogpostFormData = {
        Title: '',
        ShortDescription: '',
        Content: '',
        UrlHandle: '',
        FeaturedImageUrl: '',
        PublishedDate: new Date(),
        Author: '',
        Ticket:'',
        IsVisible: false
      };
    }
  }

}
