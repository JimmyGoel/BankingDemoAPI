import { HttpClient,  HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IUserEntity } from '../_modle/IuserEntity';

// const httpOptions = {
//   header: new HttpHeaders({
//     Authorization: 'bearer ' + JSON.parse(localStorage.getItem('users') as string).token
//   })
// }
@Injectable({
  providedIn: 'root'
})

export class UserentityService {
  baseUrl = environment.apiURl;
  constructor(private httpclient: HttpClient) { }

  getUserEntity() {
   return this.httpclient.get<IUserEntity[]>(this.baseUrl + 'user');
  }

  getUserEntityByID(Id:any) {
   return this.httpclient.get<IUserEntity>(this.baseUrl + 'user/'+Id);
  }
}
