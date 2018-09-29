import { Component, OnInit } from '@angular/core';
import { CartItemViewModel } from 'src/app/webapi/models/cart-item-view-model';
import { isNullOrUndefined } from 'util';

@Component({
  selector: 'app-cart-detail',
  templateUrl: './cart-detail.component.html',
  styleUrls: ['./cart-detail.component.css']
})
export class CartDetailComponent implements OnInit {

  cartItemList: CartItemViewModel[] = [];

  constructor() { }

  ngOnInit() {
    this.getCartItems();
  }

  getCartItems() {
    debugger;
    this.cartItemList = JSON.parse(localStorage.getItem('o2ccartItems'));
  }

  updateCartItem(itemId, val) {
    debugger;
    const cartItems = JSON.parse(localStorage.getItem('o2ccartItems'));
    const item = cartItems.find(x => x.itemId === itemId);
    if (!isNullOrUndefined(item)) {
      if (val === 'inc') {
        item.itemQuantity += 100;
      }
      if (val === 'dec') {
        item.itemQuantity -= 100;
      }
      if (val === 'clean') {
        item.isCleaned = true;
      }
      if (val === 'withoutClean') {
        item.isCleaned = false;
      }
      item.price = this.updatePrice(item);
      cartItems[cartItems.indexOf(item)] = item;
      localStorage.setItem('o2ccartItems', JSON.stringify(cartItems));
      this.getCartItems();
    }
  }

  updatePrice(item) {
    return item.isCleaned ? ((item.itemQuantity * item.orginalCleanPrice) / 1000) :
      (item.itemQuantity * item.originalPrice) / 1000;
  }
}
