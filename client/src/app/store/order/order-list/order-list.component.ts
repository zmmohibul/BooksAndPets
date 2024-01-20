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
      }
    }
  }
}
