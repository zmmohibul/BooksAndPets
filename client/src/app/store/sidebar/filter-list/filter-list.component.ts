import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MdbCheckboxModule } from 'mdb-angular-ui-kit/checkbox';

@Component({
  selector: 'app-filter-list',
  standalone: true,
  imports: [CommonModule, MdbCheckboxModule],
  templateUrl: './filter-list.component.html',
  styleUrls: ['./filter-list.component.scss'],
})
export class FilterListComponent {}
