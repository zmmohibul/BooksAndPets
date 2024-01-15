import { Component, Input, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MdbCheckboxModule } from 'mdb-angular-ui-kit/checkbox';
import { MdbRippleModule } from 'mdb-angular-ui-kit/ripple';
import { FilterListItem } from '../../../models/utils/filterListItem';
import { FilterListTypes } from '../../../models/utils/filterListTypes';

@Component({
  selector: 'app-filter-list',
  standalone: true,
  imports: [CommonModule, MdbCheckboxModule, MdbRippleModule],
  templateUrl: './filter-list.component.html',
  styleUrls: ['./filter-list.component.scss'],
})
export class FilterListComponent {
  @Input() set listItems(list: FilterListItem[]) {
    this._listItems.set(list);
  }
  _listItems: WritableSignal<FilterListItem[]> = signal([]);

  @Input() set filterType(type: FilterListTypes) {
    this._filterType.set(type);
  }
  _filterType: WritableSignal<FilterListTypes> = signal(FilterListTypes.Author);
}
