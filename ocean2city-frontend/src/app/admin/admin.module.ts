import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { SharedModule } from '../shared/shared.module';

import { AdminRoutingModule } from './admin-routing.module';
import { CategoryListComponent, CategoryComponent } from './index.admin';
import { CategoryServiceApp } from 'src/app/admin/category/shared/category.serviceApp';
import { ItemListComponent } from './item/item-list/item-list.component';
import { ItemComponent } from './item/item/item.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    BrowserModule,
    AdminRoutingModule,
    SharedModule
  ],
  declarations: [CategoryListComponent, CategoryComponent, ItemListComponent, ItemComponent],
  providers: [CategoryServiceApp]
})
export class AdminModule { }
