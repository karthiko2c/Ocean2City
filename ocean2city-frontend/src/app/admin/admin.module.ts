import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';

import { AdminRoutingModule } from './admin-routing.module';
import { CategoryListComponent, CategoryComponent, ItemComponent, ItemListComponent } from './index.admin';
import { CategoryServiceApp } from 'src/app/admin/category/shared/category.serviceApp';

@NgModule({
  imports: [
    FormsModule,
    AdminRoutingModule,
    SharedModule
  ],
  declarations: [CategoryListComponent, CategoryComponent, ItemListComponent, ItemComponent],
  providers: [CategoryServiceApp]
})
export class AdminModule { }
