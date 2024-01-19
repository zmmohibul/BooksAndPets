import { Injectable, signal, WritableSignal } from '@angular/core';
import { CartItem } from '../models/utils/cartItem';
import { ToastrService } from 'ngx-toastr';

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
  constructor(private toastr: ToastrService) {
    // this.cartItems().push(this.itm)
  }

  toggleShowCart() {
    this.showCart.set(!this.showCart());
  }

  addItemToCart(cartItem: CartItem) {
    for (let item of this.cartItems()) {
      if (
        item.productId === cartItem.productId &&
        item.measureOption === cartItem.measureOption
      ) {
        if (cartItem.quantityInStock === item.quantity) {
          this.toastr.info(
            'Your desired quantity is unavailable at the moment.',
            'Out of stock',
          );
          return;
        }

        item.quantity += cartItem.quantity;
        this.cartItems.set(this.cartItems());
        return;
      }
    }

    if (cartItem.quantityInStock === 0) {
      this.toastr.info('The product is currently unavailable.', 'Out of stock');
      return;
    }
    this.cartItems().push(cartItem);
    this.cartItems.set(this.cartItems());

    if (this.cartItems().length === 1) {
      this.showCart.set(true);
    }

    if (!this.showCart()) {
      this.toastr.success(
        `${cartItem.name}(${cartItem.measureOption})`,
        'Added to cart',
      );
    }
  }

  incrementItemQuantity(cartItem: CartItem) {
    for (let item of this.cartItems()) {
      if (
        item.productId === cartItem.productId &&
        item.measureOption === cartItem.measureOption
      ) {
        if (cartItem.quantityInStock === item.quantity) {
          this.toastr.info(
            'Your desired quantity is unavailable at the moment.',
            'Out of stock',
          );
          return;
        }

        item.quantity++;
        this.cartItems.set(this.cartItems());
        if (!this.showCart()) {
          this.toastr.success(
            `${cartItem.name}(${cartItem.measureOption})`,
            'Added to cart',
          );
        }
        return;
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

    if (res.length !== this.cartItems().length) {
      this.toastr.error(
        `${cartItem.name}(${cartItem.measureOption})`,
        'Removed from Cart',
      );
    }
    this.cartItems.set(res);
  }
}
