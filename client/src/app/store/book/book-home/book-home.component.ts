import {
  Component,
  HostListener,
  OnInit,
  signal,
  WritableSignal,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoryListComponent } from '../../sidebar/category-list/category-list.component';
import { Category } from '../../../models/product-aggregate/category-models/category';
import { CategoryService } from '../../../services/category.service';
import { ProductsPageContainerComponent } from '../../product/products-page-container/products-page-container.component';
import { FilterListComponent } from '../../sidebar/filter-list/filter-list.component';
import { BookCardComponent } from '../book-card/book-card.component';
import { BookService } from '../../../services/book.service';
import { HttpQueryParameter } from '../../../models/utils/HttpQueryParameter';
import { Book } from '../../../models/book-aggregate/book-models/book';

@Component({
  selector: 'app-book-home',
  standalone: true,
  imports: [
    CommonModule,
    CategoryListComponent,
    ProductsPageContainerComponent,
    FilterListComponent,
    BookCardComponent,
  ],
  templateUrl: './book-home.component.html',
  styleUrls: ['./book-home.component.scss'],
})
export class BookHomeComponent implements OnInit {
  categories: WritableSignal<Category[]> = signal([]);
  bookQueryParameters = new HttpQueryParameter();
  books: Book[] = [];

  constructor(
    public categoryService: CategoryService,
    public bookService: BookService,
  ) {}
  ngOnInit() {
    this.loadCategories(1);
    this.loadBooks();
  }

  loadBooks() {
    this.bookService.getAllBooks(this.bookQueryParameters).subscribe({
      next: (data) => {
        this.books = data.data.items;
        console.log(this.books);
      },
    });
  }

  loadCategories(departmentId: number) {
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
    this.loadCategories(departmentId);
  }

  public getScreenWidth: any;
  public getScreenHeight: any;
  @HostListener('window:resize', ['$event'])
  onWindowResize() {
    this.getScreenWidth = window.innerWidth;
    this.getScreenHeight = window.innerHeight;
  }
}
