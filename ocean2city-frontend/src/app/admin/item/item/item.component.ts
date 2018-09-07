import { Component, OnInit } from '@angular/core';
import { ItemViewModel } from 'src/app/shared/customModels/item-view-model';
import { ActivatedRoute, Params } from '@angular/router';
import { isNullOrUndefined } from 'util';
import { ItemServiceApp } from 'src/app/admin/item/shared/item.ServiceApp';
import { Status } from 'src/app/app.enum';
import { AppConstants } from 'src/app/shared/constants/constant.variable';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.css']
})
export class ItemComponent implements OnInit {

  itemModel: ItemViewModel = {} as ItemViewModel;
  image: any;

  constructor(private itemServiceApp: ItemServiceApp, private route: ActivatedRoute) { }

  ngOnInit() {
    this.getItemById();
  }

  getItemById() {
    this.route.params.subscribe((params: Params) => {
      const itemId = params['itemId'];
      if (!isNullOrUndefined(itemId)) {
        this.itemServiceApp.getItemById(itemId).subscribe(
          (data) => {
            if (data.status === Status.Success) {
              this.itemModel = data.body;
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

  onSubmit(itemForm) {
    if (itemForm.valid) {
      if (isNullOrUndefined(this.itemModel.itemId)) {
        this.itemServiceApp.addItem(AppConstants.uriForAddItem, this.itemModel, this.image).
          subscribe(
          (data) => {
            if (data.body.status === Status.Success) {
              //  this.showItemList();
            } else {
              // this.msgService.showError('Error');
            }
          });
      } else {
        this.itemServiceApp.updateItem(AppConstants.uriForUpdateItem, this.itemModel, this.image)
          .subscribe(
          (data) => {
            if (data.body.status === Status.Success) {
              // this.showItemList();
            } else {
              // this.msgService.showError('Error');
            }
          });
      }
    }
  }
}
