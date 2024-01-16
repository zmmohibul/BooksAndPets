import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SidebarService } from '../../../services/sidebar.service';
import { CartService } from '../../../services/cart.service';

@Component({
  selector: 'app-products-page-container',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './products-page-container.component.html',
  styleUrls: ['./products-page-container.component.scss'],
})
export class ProductsPageContainerComponent {
  showCart = false;
  constructor(
    public sidebarService: SidebarService,
    public cartService: CartService,
  ) {}

  toggleCart() {
    this.showCart = !this.showCart;
  }
}
