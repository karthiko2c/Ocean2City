import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CategoryListComponent, CategoryComponent } from './index.admin';

const routes: Routes = [
  {
    path: '',
    component: CategoryListComponent
  },
  {
    path: 'category',
    component: CategoryComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
