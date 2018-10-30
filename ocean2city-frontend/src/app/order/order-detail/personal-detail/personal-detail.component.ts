import { Component, OnInit } from '@angular/core';
import { UserViewModel } from 'src/app/webapi/models/user-view-model';

@Component({
  selector: 'app-personal-detail',
  templateUrl: './personal-detail.component.html',
  styleUrls: ['./personal-detail.component.css']
})
export class PersonalDetailComponent implements OnInit {

  userDetail: UserViewModel = {} as UserViewModel;

  constructor() { }

  ngOnInit() {
  }

  onSubmit(personalDetailForm) {
    if (personalDetailForm.valid) {
      debugger;
      localStorage.setItem('o2cpersonalDetail', JSON.stringify(this.userDetail));
    }
  }

}
