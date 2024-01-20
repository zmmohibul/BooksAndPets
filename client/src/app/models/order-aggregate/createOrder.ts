import { CreateOrderItem } from './createOrderItem';

export interface CreateOrder {
  addressId: number;
  orderItems: CreateOrderItem[];
}
