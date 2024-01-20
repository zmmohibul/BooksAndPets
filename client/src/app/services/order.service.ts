import { Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { CartService } from './cart.service';
import { CreateOrder } from '../models/order-aggregate/createOrder';
import { CreateOrderItem } from '../models/order-aggregate/createOrderItem';
import { Order } from '../models/order-aggregate/order';
import { map, of, tap } from 'rxjs';
import { PagedData } from '../models/utils/PagedData';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  baseUrl = environment.apiUrl;
  orderCache: Order[] = [];
  constructor(
    private http: HttpClient,
    private cartService: CartService,
  ) {}

  getAllOrders() {
    if (this.orderCache.length) {
      return of(this.orderCache);
    }

    return this.http.get<PagedData<Order>>(`${this.baseUrl}/orders`).pipe(
      map((response) => {
        this.orderCache = response.items;
        return response.items;
      }),
    );
  }

  createOrder(addressId: number) {
    const createOrder: CreateOrder = { addressId, orderItems: [] };
    for (let item of this.cartService.cartItems()) {
      const orderItem: CreateOrderItem = {
        productId: item.productId,
        quantity: item.quantity,
        measureTypeId: item.measureTypeId,
        measureOptionId: item.measureOptionId,
      };

      createOrder.orderItems.push(orderItem);
    }

    return this.http.post<Order>(`${this.baseUrl}/orders`, createOrder).pipe(
      tap((response) => {
        this.orderCache = [...this.orderCache, response];
      }),
    );
  }
}
