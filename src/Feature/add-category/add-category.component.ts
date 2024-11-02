import { Component } from '@angular/core';
import { BlogpostModel } from '../../Core/Model/Blogpost-Model';
import { BlogpostService } from '../../Core/Services/blogpost.service';


import { NgForm } from '@angular/forms';

import { Block } from '@angular/compiler';
@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrl: './add-category.component.css'
})
export class AddCategoryComponent {
  successMessageVisible  = false;
  isPopupVisible  = false; 
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
    PublishedDate: new Date(), 
    Author: '',
    Ticket:'',
    IsVisible: false 
  };

  constructor(private blogpostService: BlogpostService) {}

  openPopup(blog?: BlogpostModel): void {
    this.isPopupVisible = true;
    if (blog) {
  
      this.blogpostFormData = {
        Title: blog.Title || '', 
        ShortDescription: blog.ShortDescription || '',
        Content: blog.Content || '',
        UrlHandle: blog.UrlHandle || '',
        FeaturedImageUrl: blog.FeaturedImageUrl || '', 
        PublishedDate: blog.PublishedDate || new Date(), 
        Author: blog.Author || '', 
        Ticket:blog.Ticket || '',
        IsVisible: blog.IsVisible !== undefined ? blog.IsVisible : false 
      };
    } else {
     
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
