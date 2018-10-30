import { Component, OnInit } from '@angular/core';
import { CartItemViewModel } from 'src/app/webapi/models/cart-item-view-model';
import { isNullOrUndefined } from 'util';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cart-detail',
  templateUrl: './cart-detail.component.html',
  styleUrls: ['./cart-detail.component.css']
})
export class CartDetailComponent implements OnInit {

  cartItemList: CartItemViewModel[] = [];
  personalDetail: any;
  address: any;

  constructor(private router: Router) { }

  ngOnInit() {
    this.getCartItems();
  }

  getCartItems() {
    debugger;
    this.cartItemList = JSON.parse(localStorage.getItem('o2ccartItems'));
    if (!isNullOrUndefined(this.cartItemList)) {
      this.getPersonalDetail();
      this.getAddress();
    }
  }

  getPersonalDetail() {
    this.personalDetail = JSON.parse(localStorage.getItem('o2cpersonalDetail'));
  }

  getAddress() {
    this.address = JSON.parse(localStorage.getItem('o2caddressDetail'));
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

  addPersonalDetails() {
    this.router.navigate(['order/personal-detail']);
  }

  addAddress() {
    this.router.navigate(['order/order-detail/address']);
  }
}
