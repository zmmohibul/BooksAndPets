import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MdbCheckboxModule } from 'mdb-angular-ui-kit/checkbox';
import { MdbRippleModule } from 'mdb-angular-ui-kit/ripple';

@Component({
  selector: 'app-filter-list',
  standalone: true,
  imports: [CommonModule, MdbCheckboxModule, MdbRippleModule],
  templateUrl: './filter-list.component.html',
  styleUrls: ['./filter-list.component.scss'],
})
export class FilterListComponent {}
