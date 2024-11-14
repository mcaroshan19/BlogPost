import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {

  private isLoggedInSubject = new BehaviorSubject<boolean>(false);
  public isLoggedIn$ = this.isLoggedInSubject.asObservable();
   
  
  setLoginStatus(status: boolean): void {
    this.isLoggedInSubject.next(status);
  }
  constructor() { }


  checkLoginStatus(): void {
    const token = localStorage.getItem('authToken');
    if (token) {
      this.setLoginStatus(true);
    } else {
      this.setLoginStatus(false);
    }
  }

  // logout(): void {
  //   localStorage.removeItem('authToken');
  //   this.setLoginStatus(false);
  // }


}
