import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SingupService {



private baseurl: string = "https://localhost:7254/api/User";


constructor(private http: HttpClient) { }

Singup(signup: any) {
  return this.http.post<any>(`${this.baseurl}/register`, signup);
}

Login(login:any)
{
  return this.http.post<any>(`${this.baseurl}/login`, login);
}

}
