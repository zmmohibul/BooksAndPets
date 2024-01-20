import { Component, OnInit, signal, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Order } from '../../../models/order-aggregate/order';
import { OrderItem } from '../../../models/order-aggregate/orderItem';
import { OrderService } from '../../../services/order.service';
import { OrderSummaryComponent } from '../order-summary/order-summary.component';

@Component({
  selector: 'app-order-list',
  standalone: true,
  imports: [CommonModule, OrderSummaryComponent],
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.scss'],
})
export class OrderListComponent implements OnInit {
  orders: WritableSignal<Order[]> = signal([]);
  orderItems: WritableSignal<OrderItem[]> = signal([]);
  orderInView: Order | null = null;

  constructor(private orderService: OrderService) {}
  ngOnInit(): void {
    this.orderService.getAllOrders().subscribe({
      next: (orders) => {
        console.log(orders);
        this.orders.set(orders);
      },
    });
  }

  onOrderClick(id: number) {
    for (let order of this.orders()) {
      if (order.id === id) {
        this.orderItems.set(order.orderItems);
        this.orderInView = order;
      }
    }
  }

  onCancelOrderClick() {
    if (!this.orderInView) {
      return;
    }

    this.orderService.cancelOrder(this.orderInView.id).subscribe({
      next: () => {
        for (let order of this.orders()) {
          if (order.id === this.orderInView?.id) {
            order.status = 'Cancelled';
            this.orders.set(this.orders());
            break;
          }
        }
      },
    });
  }

  showCancelOrderButton() {
    return (
      this.orderInView &&
      this.orderInView.status !== 'Shipped' &&
      this.orderInView.status !== 'Cancelled' &&
      this.orderInView.status !== 'Delivered'
    );
  }
}
