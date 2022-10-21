import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  loginmodule: any = {}
  logedInUser:string="";
  IsLogedIn: boolean = false;
  constructor(public AccountService: AccountService) { }

  ngOnInit(): void {
    this.getCurrentUser();
  }
  login() {
    this.AccountService.login(this.loginmodule).subscribe((response) => {
      console.log(response);
      debugger;
      this.IsLogedIn = true;
      this.logedInUser=JSON.parse(localStorage.getItem('users') as string)['userName']
    }, error => {
      console.log(error);
    })
    console.log(this.loginmodule);
  }
  logout() {
    this.AccountService.logout();
    this.IsLogedIn = false;
  }
  getCurrentUser() {
    this.AccountService.currentUser$.subscribe(user => {
      this.IsLogedIn = !!user;
    },error=>{
      console.log(error);
    })
  }
}
