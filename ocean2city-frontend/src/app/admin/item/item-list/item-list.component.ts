import { Component, OnInit } from '@angular/core';
import { ItemViewModel } from 'src/app/shared/customModels/item-view-model';
import { ItemServiceApp } from 'src/app/admin/item/shared/item.ServiceApp';
import { Status } from 'src/app/app.enum';

@Component({
  selector: 'app-item-list',
  templateUrl: './item-list.component.html',
  styleUrls: ['./item-list.component.css']
})
export class ItemListComponent implements OnInit {

  itemList: ItemViewModel[] = [];

  constructor(private itemServiceApp: ItemServiceApp) { }

  ngOnInit() {
    this.getItemList();
  }

  getItemList() {
    this.itemServiceApp.getItemList().subscribe(
      (data) => {
        if (data.status === Status.Success) {
          this.itemList = data.body;
        } else {
          // this.msgService.showError('Error');
        }
      }
    );
  }

}
