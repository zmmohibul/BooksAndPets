import { Component, OnInit, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoryListComponent } from '../../sidebar/category-list/category-list.component';
import { Category } from '../../../models/category-models/category';
import { CategoryService } from '../../../services/category.service';

@Component({
  selector: 'app-book-home',
  standalone: true,
  imports: [CommonModule, CategoryListComponent],
  templateUrl: './book-home.component.html',
  styleUrls: ['./book-home.component.scss'],
})
export class BookHomeComponent implements OnInit {
  categories: WritableSignal<Category[]> = signal([]);

  constructor(public categoryService: CategoryService) {}
  ngOnInit() {
    this.loadAllCategories(1);
  }

  loadAllCategories(departmentId: number) {
    this.categoryService.getAllCategories(departmentId).subscribe({
      next: (data) => {
        this.categories.set(data);
        console.log(this.categories());
      },
    });
  }

  async onCategoryClick(categoryId: number) {
    let list: Category[] = [];
    let category = await this.categoryService
      .getCategoryById(1, categoryId)
      .toPromise();
    list.unshift(category!);
    while (category?.parentId != null) {
      category = await this.categoryService
        .getCategoryById(1, category.parentId)
        .toPromise();
      list.unshift(category!);
    }

    this.categories.set(list);
    console.log(list);
  }

  onDepartmentClick(departmentId: number) {
    this.loadAllCategories(departmentId);
  }
}