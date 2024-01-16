import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { BookData } from '../models/book-aggregate/book-models/bookData';
import { HttpQueryParameter } from '../models/utils/HttpQueryParameter';
import { of, tap } from 'rxjs';
import { BookDetails } from '../models/book-aggregate/book-models/bookDetails';

@Injectable({
  providedIn: 'root',
})
export class BookService {
  baseUrl = environment.apiUrl;
  bookCache = new Map<string, BookData>();
  bookDetailsCache = new Map<number, BookDetails>();

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

  getBookById(id: number) {
    let book = this.bookDetailsCache.get(id);
    if (book) {
      return of(book);
    }

    return this.http.get<BookDetails>(`${this.baseUrl}/books/${id}`).pipe(
      tap((response) => {
        this.bookDetailsCache.set(id, response);
      }),
    );
  }
}
