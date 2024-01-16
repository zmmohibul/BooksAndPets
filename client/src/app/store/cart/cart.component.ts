import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss'],
})
export class CartComponent {
  constructor(public cartService: CartService) {}

  incrementItemQuantity(productId: number) {
    this.cartService.incrementItemQuantity(productId);
  }
  decrementItemQuantity(productId: number) {
    this.cartService.decrementItemQuantity(productId);
  }
  removeItem(productId: number) {
    this.cartService.removeItemFromCart(productId);
  }
}
