import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MyComponentComponent } from '../Core/my-component/my-component.component';

import { AddCategoryComponent } from '../Feature/list-category/add-category/add-category.component';
import { HomeComponent } from '../Core/home/home.component';
import { ListCategoryComponent } from '../Feature/list-category/list-category.component';
import { EditComponent } from '../Feature/edit/edit.component';
import { SingupComponent } from '../Feature/singup/singup.component';
import { LoginComponent } from '../Feature/login/login/login.component';
import { authGuard } from '../Core/Authgard/auth.guard';
import { UserComponent } from './user/user.component';


const routes: Routes = [

  
  {
    path: 'List',
    component: ListCategoryComponent,
    canActivate: [authGuard] 
  
  }, 
  {
    path:'My' ,
    component: MyComponentComponent,
    canActivate: [authGuard] 
  },
  {
    path:'add-category',
    component: AddCategoryComponent, title:'Edit',
    canActivate: [authGuard] 
  },
  {
    path:'home' ,
    component: HomeComponent,
    canActivate: [authGuard] 
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
  },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  {
    path:'app-user',
    component: UserComponent
  }
  

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
