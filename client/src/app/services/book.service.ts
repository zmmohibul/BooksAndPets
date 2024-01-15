import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { BookData } from '../models/book-aggregate/book-models/bookData';
import { HttpQueryParameter } from '../models/utils/HttpQueryParameter';
import { of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BookService {
  baseUrl = environment.apiUrl;
  bookCache = new Map<string, BookData>();

  constructor(private http: HttpClient) {}

  getAllBooks(queryParameters: HttpQueryParameter) {
    let queryString = queryParameters.getQueryString();
    let params = queryParameters.getHttpParamsObject();

    const data = this.bookCache.get(queryString);
    if (data) {
      return of(data);
    }

    return this.http.get<BookData>(`${this.baseUrl}/books`, { params }).pipe(
      tap((response) => {
        this.bookCache.set(queryString, response);
      }),
    );
  }
}
