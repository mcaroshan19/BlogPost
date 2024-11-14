import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RequestModel } from '../Model/RequestModel';
import { ResponseModel } from '../Model/ResponseModel';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class PostDataService {
 private  url= 'https://localhost:7254/api/Categories';

  constructor(private http:HttpClient) { }

  
  addCategory(category: RequestModel): Observable<RequestModel[]> {

   
    return this.http.post<RequestModel[]>(this.url, category);}
   
  
    public getData(): Observable<ResponseModel[]>{
      return this.http.get<ResponseModel[]>(this.url );

    }

    updateCategory(id: number, category: RequestModel): Observable<RequestModel> {
      return this.http.put<RequestModel>(`${this.url}/${id}`, category).pipe(
      
      );
    }
  

    getCategory(id: number): Observable<RequestModel> {
      return this.http.get<RequestModel>(`${this.url}/${id}`);
    }
    deleteCategory(id: number): Observable<RequestModel> {
      return this.http.delete<RequestModel>(`${this.url}/${id}`);
    }
    

}
