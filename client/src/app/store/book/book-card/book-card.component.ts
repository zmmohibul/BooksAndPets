import { Component, Input, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MdbDropdownModule } from 'mdb-angular-ui-kit/dropdown';
import { RouterLink } from '@angular/router';
import { Book } from '../../../models/book-aggregate/book-models/book';
import { Category } from '../../../models/product-aggregate/category-models/category';
import { CartService } from '../../../services/cart.service';
import { CartItem } from '../../../models/utils/cartItem';
import { PriceOption } from '../../../models/utils/priceOption';

@Component({
  selector: 'app-book-card',
  standalone: true,
  imports: [CommonModule, MdbDropdownModule, RouterLink],
  templateUrl: './book-card.component.html',
  styleUrls: ['./book-card.component.scss'],
})
export class BookCardComponent {
  @Input() set book(item: Book) {
    this._book.set(item);
  }
  _book: WritableSignal<Book | null> = signal(null);
  priceOption = PriceOption;

  constructor(private cartService: CartService) {}

  getPaperbackPrice() {
    if (!this._book()) return;
    for (let price of this._book()?.priceList!) {
      if (price.measureOption === this.priceOption.Paperback) {
        return price.unitPrice;
      }
    }
    return;
  }

  getHardcoverPrice() {
    if (this._book() == null) return;
    for (let price of this._book()?.priceList!) {
      if (price.measureOption === this.priceOption.Hardcover) {
        return price.unitPrice;
      }
    }
    return;
  }

  addItemToCart(measureOption: string) {
    const book = this._book();
    if (!book) return;

    const price = book.priceList.find((p) => p.measureOption === measureOption);
    if (!price) return;

    const item = new CartItem(
      book.id,
      book.name,
      book.mainPictureUrl,
      price.measureTypeId,
      price.measureOptionId,
      price.measureOption,
      price.unitPrice,
      1,
      price.quantityInStock,
    );

    this.cartService.addItemToCart(item);
  }
}
