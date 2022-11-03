import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { isSubscription } from 'rxjs/internal/Subscription';
import { environment } from 'src/environments/environment';
import { IUserEntity } from '../_modle/IuserEntity';
import { IUserDetail } from '../_modle/user';

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
  member: IUserEntity[] = [];
  constructor(private httpclient: HttpClient) { }

  getUserEntity() {
    if (this.member.length > 0) return of(this.member)
    return this.httpclient.get<IUserEntity[]>(this.baseUrl + 'user').pipe(
      map(member => {
        this.member = member;
        return member;
      })
    )
  }

  getUserEntityByID(Id: any) {
    const member = this.member.find(x => x.Id === Id);
if(member!==undefined){
  return of(member);
}
    return this.httpclient.get<IUserEntity>(this.baseUrl + 'user/' + Id);
  }
  userUpdatedetails(userDetail: any) {
    return this.httpclient.put(this.baseUrl + 'user/update-user', userDetail).pipe(
      map(()=>{
        const index=this.member.indexOf(userDetail);
        this.member[index]=userDetail;
      })
    );
  }
}
