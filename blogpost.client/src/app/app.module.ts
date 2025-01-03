import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MyComponentComponent } from '../Core/my-component/my-component.component';
import { AddCategoryComponent } from '../Feature/list-category/add-category/add-category.component';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ListCategoryComponent } from '../Feature/list-category/list-category.component';
import { FormsModule } from '@angular/forms';
import { HomeComponent } from '../Core/home/home.component';
import { EditComponent } from '../Feature/edit/edit.component';
import { FilterPipe } from '../Core/filter.pipe';
import { SingupComponent } from '../Feature/singup/singup.component';
import { HighlightDirective } from '../Core/highlight.directive';
import { ReactiveFormsModule } from '@angular/forms'; 

import { LoginComponent } from '../Feature/login/login/login.component';
import { UserComponent } from './user/user.component';
@NgModule({
  declarations: [
    AppComponent,
    MyComponentComponent,
    AddCategoryComponent,
    ListCategoryComponent,
    HomeComponent,
    EditComponent,
    FilterPipe,
    SingupComponent,
    LoginComponent,
    HighlightDirective,
    UserComponent

  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule 
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
