/* tslint:disable */
import { CartItemViewModel } from './cart-item-view-model';
export interface OrderDetailViewModel {
  orderId?: string;
  userId?: string;
  addressId?: string;
  deliveryDate?: string;
  deliveryTiming?: string;
  isDelivered?: boolean;
  totalAmount?: number;
  cartItems?: Array<CartItemViewModel>;
}
