/* tslint:disable */
import { UserViewModel } from './user-view-model';
import { UserAddressViewModel } from './user-address-view-model';
import { CartItemViewModel } from './cart-item-view-model';
import { OrderDetailViewModel } from './order-detail-view-model';
export interface Order {
  userViewModel?: UserViewModel;
  userAddressViewModel?: UserAddressViewModel;
  cartItemList?: Array<CartItemViewModel>;
  orderDetailViewModel?: OrderDetailViewModel;
}
