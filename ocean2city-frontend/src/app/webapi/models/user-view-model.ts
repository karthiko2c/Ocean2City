/* tslint:disable */
import { UserAddressViewModel } from './user-address-view-model';
export interface UserViewModel {
  userId?: string;
  name?: string;
  mobNo?: string;
  mailId?: string;
  userAddress?: Array<UserAddressViewModel>;
}
