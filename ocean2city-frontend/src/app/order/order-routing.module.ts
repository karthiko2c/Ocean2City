import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { OrderCategoryComponent, OrderItemComponent, ItemDetailComponent, CartDetailComponent } from './index.order';

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
