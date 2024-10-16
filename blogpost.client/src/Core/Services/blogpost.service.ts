import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BlogpostModel } from '../Model/Blogpost-Model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BlogpostService {
  private urls= 'https://localhost:7254/api/Blogposts';

  constructor(private http: HttpClient) { }

  Postdata(categoryy: BlogpostModel): Observable<BlogpostModel[]>{
    return this.http.post<BlogpostModel[]>(this.urls, categoryy);
  }
}
