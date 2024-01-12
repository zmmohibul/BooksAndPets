import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  signal,
  WritableSignal,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { Category } from '../../../models/category-models/category';
import { Department } from '../../../models/department-models/department';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss'],
})
export class CategoryListComponent implements OnInit {
  @Output() categoryClick = new EventEmitter<number>();
  @Output() departmentClick = new EventEmitter<number>();
  @Input() set categoryList(list: Category[]) {
    this._categoryList.set(list);
  }

  currentCategoryId = signal(0);
  department: Department = { id: 1, name: 'Book' };
  _categoryList: WritableSignal<Category[]> = signal([]);

  ngOnInit(): void {}

  hasSubCategory() {
    const listLen = this._categoryList().length;
    return (
      listLen &&
      this._categoryList()[listLen - 1].subCategories &&
      this._categoryList()[listLen - 1].subCategories?.length
    );
  }

  onCategoryClick(categoryId: number) {
    console.log(categoryId);
    this.categoryClick.emit(categoryId);
    this.currentCategoryId.set(categoryId);
  }

  onDepartmentClick() {
    this.departmentClick.emit(this.department.id);
    this.currentCategoryId.set(0);
  }
}
