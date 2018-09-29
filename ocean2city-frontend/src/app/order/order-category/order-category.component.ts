import { Component, OnInit } from '@angular/core';
import { Status } from 'src/app/app.enum';
import { CategoryServiceApp } from 'src/app/admin/category/shared/category.serviceApp';
import { CategoryViewModel } from 'src/app/shared/customModels/category-view-model';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-order-category',
  templateUrl: './order-category.component.html',
  styleUrls: ['./order-category.component.css']
})
export class OrderCategoryComponent implements OnInit {

  categoryList: CategoryViewModel[] = [];

  constructor(private categoryServiceApp: CategoryServiceApp, private router: Router) { }

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

  getItems(categoryId) {
    this.router.navigate(['order/order-item', categoryId]);
  }

}
