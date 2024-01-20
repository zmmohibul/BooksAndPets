import { OrderItem } from './orderItem';

export interface Order {
  id: number;
  orderItems: OrderItem[];
  orderDate: Date;
  status: string;
  addressId: number;
}
