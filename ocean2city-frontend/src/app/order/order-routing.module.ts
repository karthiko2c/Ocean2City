import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {
  OrderCategoryComponent, OrderItemComponent, ItemDetailComponent, CartDetailComponent,
  AddressComponent, OtpVerifyComponent, PersonalDetailComponent
} from './index.order';

const routes: Routes = [
  {
    path: 'order-category',
    component: OrderCategoryComponent,
  },
  {
    path: 'order-item/:categoryId',
    component: OrderItemComponent,
  },
  {
    path: 'item-detail/:itemId',
    component: ItemDetailComponent,
  },
  {
    path: 'cart-detail',
    component: CartDetailComponent,
  },
  {
    path: 'address',
    component: AddressComponent,
  },
  {
    path: 'otp-verify',
    component: OtpVerifyComponent,
  },
  {
    path: 'personal-detail',
    component: PersonalDetailComponent,
  },
  {
    path: '',
    redirectTo: 'order-category',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrderRoutingModule { }
