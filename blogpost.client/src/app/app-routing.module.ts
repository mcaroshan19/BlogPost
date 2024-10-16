import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MyComponentComponent } from '../Core/my-component/my-component.component';

import { AddCategoryComponent } from '../Feature/list-category/add-category/add-category.component';
import { HomeComponent } from '../Core/home/home.component';
import { ListCategoryComponent } from '../Feature/list-category/list-category.component';
import { EditComponent } from '../Feature/edit/edit.component';
import { SingupComponent } from '../Feature/singup/singup.component';
import { LoginComponent } from '../Feature/login/login/login.component';


const routes: Routes = [

  
  {
    path: 'List',
    component: ListCategoryComponent
  
  }, 
  {
    path:'My' ,
    component: MyComponentComponent,
  },
  {
    path:'add-category',
    component: AddCategoryComponent, title:'Edit'
  },
  {
    path:'home' ,
    component: HomeComponent
  },
  {
    path:'edit' ,
    component: EditComponent
  },
  {
    path:'singup',
    component:SingupComponent
  },
  {
    path:'login',
    component:LoginComponent
  }
  

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
