import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IUserDetail } from '../_modle/user';


@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl =environment.apiURl;

  private currentUserSource = new ReplaySubject<IUserDetail>(1);
  currentUser$ = this.currentUserSource.asObservable();


  constructor(private http: HttpClient) { }


  login(loginmodule: any) {
    return this.http.post<IUserDetail>(this.baseUrl + 'Account/Login-user', loginmodule).pipe
      (
        map((resp: IUserDetail) => {
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
    return this.http.post<IUserDetail>(this.baseUrl + 'Account/registor-user', loginmodule).pipe
      (
        map((resp: IUserDetail) => {
          debugger;
          const user = resp;
          if (user) {
            localStorage.setItem("users", JSON.stringify(user));
            this.currentUserSource.next(user);
          }
        })
      );
  }

  setCurrentUser(user:IUserDetail){
    this.currentUserSource.next(user);
  }
  logout(){
    localStorage.removeItem("users");
    this.currentUserSource.next(null!);
  }
}
