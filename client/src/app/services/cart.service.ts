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
  cartItems: WritableSignal<CartItem[]> = signal([]);
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
      if (
        item.productId === cartItem.productId &&
        item.measureOption === cartItem.measureOption
      ) {
        item.quantity += cartItem.quantity;
        this.cartItems.set(this.cartItems());
        return;
      }
    }

    this.cartItems().push(cartItem);
    this.cartItems.set(this.cartItems());
  }

  incrementItemQuantity(cartItem: CartItem) {
    for (let item of this.cartItems()) {
      if (
        item.productId === cartItem.productId &&
        item.measureOption === cartItem.measureOption
      ) {
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
  decrementItemQuantity(cartItem: CartItem) {
    for (let item of this.cartItems()) {
      if (
        item.productId === cartItem.productId &&
        item.measureOption === cartItem.measureOption
      ) {
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

  removeItemFromCart(cartItem: CartItem) {
    console.log(this.cartItems());
    const res = [];
    for (let itm of this.cartItems()) {
      if (itm.productId !== cartItem.productId) {
        res.push(itm);
      } else if (itm.measureOption !== cartItem.measureOption) {
        res.push(itm);
      }
    }
    this.cartItems.set(res);
  }
}
