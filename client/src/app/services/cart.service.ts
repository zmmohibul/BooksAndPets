import { Injectable, signal, WritableSignal } from '@angular/core';
import { CartItem } from '../models/utils/cartItem';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  showCart = signal(false);
  itm = new CartItem(
    1,
    'Fahrenheit 451',
    'https://m.media-amazon.com/images/I/61z7RDG3OIL._AC_UY327_FMwebp_QL65_.jpg',
    'paperback',
    800,
    1,
    27,
  );
  cartItems: WritableSignal<CartItem[]> = signal([this.itm, this.itm]);
  constructor() {
    // this.cartItems().push(this.itm)
  }

  toggleShowCart() {
    this.showCart.set(!this.showCart());
  }

  addItemToCart(cartItem: CartItem) {
    if (cartItem.quantity === cartItem.quantityInStock) {
      console.log('Out of stock!');
      return;
    }

    for (let item of this.cartItems()) {
      if (item.productId === cartItem.productId) {
        item.quantity += cartItem.quantity;
        this.cartItems.set(this.cartItems());
        return;
      }
    }

    this.cartItems().push(cartItem);
    this.cartItems.set(this.cartItems());
  }

  incrementItemQuantity(productId: number) {
    for (let item of this.cartItems()) {
      if (item.productId === productId) {
        if (item.quantityInStock === item.quantity) {
          console.log('Out of stock!');
          return;
        } else {
          item.quantity++;
          this.cartItems.set(this.cartItems());
          return;
        }
      }
    }
  }
  decrementItemQuantity(productId: number) {
    for (let item of this.cartItems()) {
      if (item.productId === productId) {
        if (item.quantity === 1) {
          return;
        } else {
          item.quantity--;
          this.cartItems.set(this.cartItems());
          return;
        }
      }
    }
  }

  removeItemFromCart(productId: number) {
    const result = this.cartItems().filter(
      (item) => item.productId !== productId,
    );
    this.cartItems.set(result);
  }
}
