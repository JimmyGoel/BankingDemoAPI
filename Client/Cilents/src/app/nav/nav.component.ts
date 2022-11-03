import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';


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
  constructor(public AccountService: AccountService, private router: Router,
    private toaster: ToastrService) { }

  ngOnInit(): void {
    this.getCurrentUser();
  }
  login() {
    this.AccountService.login(this.loginmodule).subscribe((response) => {
      console.log(response);
      debugger;
      this.IsLogedIn = true;
      // this.AccountService.currentUser$.pipe(take(2)).subscribe((membera:any) => {
      //   debugger;
      //   // This is for user login detail below not corret
      //   this.logedInUser=membera.userName;
      // })
      // this.logedInUser=JSON.parse(localStorage.getItem('users') as string)['userName']
      this.router.navigateByUrl('/members');
    }, error => {
      console.log(error);
      this.toaster.error(error.error);
    })
    console.log(this.loginmodule);
  }
  logout() {
    this.AccountService.logout();
    this.IsLogedIn = false;
    this.router.navigate(['/']);
  }
  getCurrentUser() {
    this.AccountService.currentUser$.subscribe(user => {
      this.IsLogedIn = !!user;
    },error=>{
      console.log(error);
    })
  }
}
