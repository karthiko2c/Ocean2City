import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { CategoryViewModel } from 'src/app/shared/customModels/category-view-model';
import { CategoryServiceApp } from 'src/app/admin/category/shared/category.serviceApp';
import { isNullOrUndefined } from 'util';
import { Status } from 'src/app/app.enum';
import { AppConstants } from 'src/app/shared/constants/constant.variable';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {

  categoryModel: CategoryViewModel = {} as CategoryViewModel;
  image: any;

  constructor(private categoryServiceApp: CategoryServiceApp, private route: ActivatedRoute) { }

  ngOnInit() {
    this.getCategoryById();
  }

  getCategoryById() {
    this.route.params.subscribe((params: Params) => {
      const categoryId = params['categoryId'];
      if (!isNullOrUndefined(categoryId)) {
        this.categoryServiceApp.getCategoryById(categoryId).subscribe(
          (data) => {
            if (data.status === Status.Success) {
              this.categoryModel = data.body;
            } else {
            //  this.msgService.showError('Error');
            }
          }
        );
      }
    });
  }

  fileChange(event) {
    const fileList: FileList = event.target.files;
    if (fileList.length > 0) {
      const file: File = fileList[0];
      this.image = file;
    }
  }

  onSubmit(categoryForm) {
    if (categoryForm.valid) {
      if (isNullOrUndefined(this.categoryModel.categoryId)) {
        this.categoryServiceApp.addCategory(AppConstants.uriForAddCategory, this.categoryModel, this.image).
          subscribe(
          (data) => {
            if (data.body.status === Status.Success) {
              //  this.showCategoryList();
            } else {
              // this.msgService.showError('Error');
            }
          });
      } else {
        this.categoryServiceApp.updateCategory(AppConstants.uriForUpdateCategory, this.categoryModel, this.image)
          .subscribe(
          (data) => {
            if (data.body.status === Status.Success) {
              // this.showCategoryList();
            } else {
              // this.msgService.showError('Error');
            }
          });
      }
    }
  }

}
