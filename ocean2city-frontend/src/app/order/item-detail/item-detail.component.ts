import { Component, OnInit } from '@angular/core';
import { ItemServiceApp } from 'src/app/admin/item/shared/item.ServiceApp';
import { Params, ActivatedRoute } from '@angular/router';
import { isNullOrUndefined } from 'util';
import { Status } from 'src/app/app.enum';
import { ItemViewModel } from 'src/app/shared/customModels/item-view-model';
import { CartItemViewModel } from 'src/app/webapi/models/cart-item-view-model';
import { element } from 'protractor';

@Component({
  selector: 'app-item-detail',
  templateUrl: './item-detail.component.html',
  styleUrls: ['./item-detail.component.css']
})
export class ItemDetailComponent implements OnInit {

  item: ItemViewModel = {} as ItemViewModel;
  cartItem: CartItemViewModel = {} as CartItemViewModel;
  isItem = false;

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
              debugger;
              this.item = data.body;
              this.isItem = true;
              const cartItems = JSON.parse(localStorage.getItem('o2ccartItems'));
              const item = cartItems.find(x => x.itemId === itemId);
              if (isNullOrUndefined(item)) {
                this.setCartItem();
              } else {
                this.cartItem = item;
              }
            } else {
              //  this.msgService.showError('Error');
            }
          }
        );
      }
    });
  }

  setCartItem() {
    this.cartItem.itemQuantity = 0;
    this.cartItem.isCleaned = false;
    this.cartItem.originalPrice = this.item.priceWithoutClean;
    this.cartItem.orginalCleanPrice = this.item.priceWithClean;
    this.cartItem.categoryId = this.item.category;
    this.cartItem.itemId = this.item.itemId;
    this.cartItem.itemName = this.item.itemName;
    this.cartItem.price = 0;
  }

  increaseQuantity() {
    this.cartItem.itemQuantity += 100;
    this.updatePrice();
  }

  decreaseQuantity() {
    if (this.cartItem.itemQuantity > 0) {
      this.cartItem.itemQuantity -= 100;
      this.updatePrice();
    }
  }

  updatePrice() {
    this.cartItem.price = this.cartItem.isCleaned ? ((this.cartItem.itemQuantity * this.cartItem.orginalCleanPrice) / 1000) :
      (this.cartItem.itemQuantity * this.cartItem.originalPrice) / 1000;
  }

  onSubmit(addCartItemForm) {
    if (addCartItemForm.valid) {
      debugger;
      const cartItems = JSON.parse(localStorage.getItem('o2ccartItems'));
      const item = cartItems.find(x => x.itemId === this.cartItem.itemId);
      if (!isNullOrUndefined(item)) {
        cartItems[cartItems.indexOf(item)] = this.cartItem;
      } else {
        cartItems.push(this.cartItem);
      }
      localStorage.setItem('o2ccartItems', JSON.stringify(cartItems));
    }
  }
}
