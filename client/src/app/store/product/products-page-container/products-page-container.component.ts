import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SidebarService } from '../../../services/sidebar.service';

@Component({
  selector: 'app-products-page-container',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './products-page-container.component.html',
  styleUrls: ['./products-page-container.component.scss'],
})
export class ProductsPageContainerComponent {
  constructor(public sidebarService: SidebarService) {}
}
