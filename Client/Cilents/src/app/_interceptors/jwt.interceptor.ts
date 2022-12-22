import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, retry, take } from 'rxjs';
import { AccountService } from '../_services/account.service';
import { IUserDetail } from '../_Interface/user';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private accountService: AccountService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let currentUser!: IUserDetail ;

    this.accountService.currentUser$.pipe(take(1)).subscribe(user => currentUser = user);
    debugger;
    if (currentUser) {
      request = request.clone(
        { headers: request.headers.set('Authorization', 'Bearer ' + currentUser.token) });
    }
    return next.handle(request).pipe(retry(3));
  }
}
