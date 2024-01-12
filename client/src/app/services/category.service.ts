import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Category } from '../models/category-models/category';
import { of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  baseUrl = environment.apiUrl;
  allCategories: Category[] = [];
  subCategories: Map<number, Category> = new Map<number, Category>();
  categoryMap: Map<number, Category> = new Map<number, Category>();

  constructor(private http: HttpClient) {}

  getAllCategories(departmentId: number) {
    if (this.allCategories.length) {
      return of(this.allCategories);
    }

    return this.http
      .get<Category[]>(
        `${this.baseUrl}/products/departments/${departmentId}/categories`,
      )
      .pipe(
        tap((categories) => {
          this.allCategories = categories;
          for (let category of categories) {
            this.categoryMap.set(category.id, category);
          }
        }),
      );
  }

  getCategoryById(departmentId: number, categoryId: number) {
    let category = this.subCategories.get(categoryId);
    if (category) {
      return of(category);
    }

    return this.http
      .get<Category>(
        `${this.baseUrl}/products/departments/${departmentId}/categories/${categoryId}`,
      )
      .pipe(
        tap((category) => {
          this.subCategories.set(categoryId, category);
          this.categoryMap.set(categoryId, category);
        }),
      );
  }

  getSubCategories(departmentId: number, categoryId: number) {
    let category = this.subCategories.get(categoryId);
    if (category) {
      return of(category);
    }

    return this.http
      .get<Category>(
        `${this.baseUrl}/products/departments/${departmentId}/categories/${categoryId}`,
      )
      .pipe(
        tap((category) => {
          this.subCategories.set(categoryId, category);
          this.categoryMap.set(categoryId, category);
        }),
      );
  }
}
