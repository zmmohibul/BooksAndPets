import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderSummaryItem } from '../../../models/utils/orderSummaryItem';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-order-summary',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './order-summary.component.html',
  styleUrls: ['./order-summary.component.scss'],
})
export class OrderSummaryComponent {
  @Input() orderId: number = 0;
  @Input() orderSummaryItems: OrderSummaryItem[] = [];
  loading = false;

  constructor() {}

  ngOnInit(): void {}

  getOrderTotal() {
    let total = 0;
    for (let item of this.orderSummaryItems) {
      total += item.unitPrice * item.quantity;
    }

    return total;
  }
}
