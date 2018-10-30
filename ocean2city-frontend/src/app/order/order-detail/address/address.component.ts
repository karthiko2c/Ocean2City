import { Component, OnInit } from '@angular/core';
import { UserAddressViewModel } from 'src/app/webapi/models/user-address-view-model';

@Component({
  selector: 'app-address',
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.css']
})
export class AddressComponent implements OnInit {

  addressDetail: UserAddressViewModel = {} as UserAddressViewModel;

  constructor() { }

  ngOnInit() {
  }

  onSubmit(addressDetailForm) {
    if (addressDetailForm.valid) {
      debugger;
      localStorage.setItem('o2caddressDetail', JSON.stringify(this.addressDetail));
    }
  }

}
