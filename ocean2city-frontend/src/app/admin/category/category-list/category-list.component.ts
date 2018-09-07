import { Component, OnInit } from '@angular/core';
import { CategoryServiceApp } from 'src/app/admin/category/shared/category.serviceApp';
import { CategoryViewModel } from 'src/app/shared/customModels/category-view-model';
import { Status } from 'src/app/app.enum';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent implements OnInit {

  categoryList: CategoryViewModel[] = [];

  constructor(private categoryServiceApp: CategoryServiceApp) { }

  ngOnInit() {
    this.getCategoryList();
  }

  getCategoryList() {
    this.categoryServiceApp.getCategoryList().subscribe(
      (data) => {
        if (data.status === Status.Success) {
          this.categoryList = data.body;
        } else {
          // this.msgService.showError('Error');
        }
      }
    );
  }

}
