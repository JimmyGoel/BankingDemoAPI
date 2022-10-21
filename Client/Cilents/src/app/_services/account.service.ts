import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, ReplaySubject } from 'rxjs';
import { UserDetail } from '../_modle/user';


@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'http://localhost:7595/api/';

  private currentUserSource = new ReplaySubject<UserDetail>(1);
  currentUser$ = this.currentUserSource.asObservable();


  constructor(private http: HttpClient) { }


  login(loginmodule: any) {
    return this.http.post<UserDetail>(this.baseUrl + 'Account/Login-user', loginmodule).pipe
      (
        map((resp: UserDetail) => {
          debugger;
          const user = resp;
          if (user) {
            localStorage.setItem("users", JSON.stringify(user));
            this.currentUserSource.next(user);
          }
        })
      );
  }

  register(loginmodule: any) {
    return this.http.post<UserDetail>(this.baseUrl + 'Account/registor-user', loginmodule).pipe
      (
        map((resp: UserDetail) => {
          debugger;
          const user = resp;
          if (user) {
            localStorage.setItem("users", JSON.stringify(user));
            this.currentUserSource.next(user);
          }
        })
      );
  }

  setCurrentUser(user:UserDetail){
    this.currentUserSource.next(user);
  }
  logout(){
    localStorage.removeItem("users");
    this.currentUserSource.next(null!);
  }
}
