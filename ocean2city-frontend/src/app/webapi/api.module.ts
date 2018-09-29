/* tslint:disable */
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { ApiConfiguration } from './api-configuration';

import { CategoryService } from './services/category.service';
import { ItemService } from './services/item.service';
import { LoginService } from './services/login.service';
import { OrderService } from './services/order.service';
import { UserService } from './services/user.service';

/**
 * Module that provides instances for all API services
 */
@NgModule({
  imports: [
    HttpClientModule
  ],
  exports: [
    HttpClientModule
  ],
  declarations: [],
  providers: [
    ApiConfiguration,
   CategoryService,
   ItemService,
   LoginService,
   OrderService,
   UserService
  ],
})
export class ApiModule { }
