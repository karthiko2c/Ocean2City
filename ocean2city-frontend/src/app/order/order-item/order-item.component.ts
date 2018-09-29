import { Component, OnInit } from '@angular/core';
import { Params, ActivatedRoute, Router } from '@angular/router';
import { isNullOrUndefined } from 'util';
import { ItemServiceApp } from 'src/app/admin/item/shared/item.ServiceApp';
import { ItemViewModel } from 'src/app/shared/customModels/item-view-model';
import { Status } from 'src/app/app.enum';
import { Jsonp } from '@angular/http/src/http';

@Component({
  selector: 'app-order-item',
  templateUrl: './order-item.component.html',
  styleUrls: ['./order-item.component.css']
})
export class OrderItemComponent implements OnInit {

  items: ItemViewModel[] = [];

  constructor(private route: ActivatedRoute, private itemServiceApp: ItemServiceApp, private router: Router) { }

  ngOnInit() {
    this.getItemByCategory();
  }

  getItemByCategory() {
    this.route.params.subscribe((params: Params) => {
      const categoryId = params['categoryId'];
      if (!isNullOrUndefined(categoryId)) {
        this.itemServiceApp.getItemByCategory(categoryId).subscribe(
          (data) => {
            if (data.status === Status.Success) {
              if (localStorage.getItem('o2ccartItems') === null) {
                debugger;
                const cartItem = [];
                localStorage.setItem('o2ccartItems', JSON.stringify(cartItem));
              }
              this.items = data.body;
            } else {
              //  this.msgService.showError('Error');
            }
          }
        );
      }
    });
  }

  getItemDetail(itemId) {
    this.router.navigate(['order/item-detail', itemId]);
  }
}
