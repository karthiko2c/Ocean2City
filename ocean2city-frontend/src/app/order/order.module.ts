import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { AdminModule } from '../admin/admin.module';

import { OrderRoutingModule } from './order-routing.module';
import { OrderCategoryComponent, OrderItemComponent, ItemDetailComponent, CartDetailComponent } from './index.order';
import { ItemServiceApp } from 'src/app/admin/item/shared/item.ServiceApp';
import { PersonalDetailComponent } from './order-detail/personal-detail/personal-detail.component';
import { AddressComponent } from './order-detail/address/address.component';
import { OtpVerifyComponent } from './order-detail/otp-verify/otp-verify.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    OrderRoutingModule,
    SharedModule,
    AdminModule
  ],
  declarations: [OrderCategoryComponent, OrderItemComponent, ItemDetailComponent,
    CartDetailComponent, PersonalDetailComponent, AddressComponent, OtpVerifyComponent],
  providers: [ItemServiceApp]
})
export class OrderModule { }
