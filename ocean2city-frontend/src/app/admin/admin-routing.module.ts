import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CategoryListComponent, CategoryComponent, ItemComponent, ItemListComponent } from './index.admin';

const routes: Routes = [
  {
    path: '',
    component: CategoryListComponent
  },
  {
    path: 'category',
    component: CategoryComponent
  },
  {
    path: 'item',
    component: ItemComponent
  },
  {
    path: 'item-list',
    component: ItemListComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
