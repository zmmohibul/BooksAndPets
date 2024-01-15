import { Component, Input, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MdbDropdownModule } from 'mdb-angular-ui-kit/dropdown';
import { RouterLink } from '@angular/router';
import { Book } from '../../../models/book-aggregate/book-models/book';
import { Category } from '../../../models/product-aggregate/category-models/category';

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

  getPaperbackPrice() {
    if (!this._book()) return;
    for (let price of this._book()?.priceList!) {
      if (price.measureOption === 'paperback') {
        return price.unitPrice;
      }
    }
    return;
  }

  getHardcoverPrice() {
    if (this._book() == null) return;
    for (let price of this._book()?.priceList!) {
      if (price.measureOption === 'hardcover') {
        return price.unitPrice;
      }
    }
    return;
  }
}
