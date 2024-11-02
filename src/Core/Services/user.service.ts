import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
 private apiUrl: string= 'https://localhost:7254/api/NavigationForm';
  constructor(private http: HttpClient) { }

  saveUser(formData: FormData): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/UserReg`, formData);
  }
}
