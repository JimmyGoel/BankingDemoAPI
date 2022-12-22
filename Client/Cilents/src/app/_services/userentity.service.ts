import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { isSubscription } from 'rxjs/internal/Subscription';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../_Interface/Ipagination';
import { IUserEntity } from '../_Interface/IuserEntity';
import { IUserDetail } from '../_Interface/user';
import { UserParams } from '../_Interface/userParams';

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
  paginationResult: any = new PaginatedResult<IUserEntity[]>();
  memoryCash = new Array();

  constructor(private httpclient: HttpClient) { }

  getUserEntity(userParam: UserParams) {
    console.log(Object.values(userParam).join('-'));
    let response = this.memoryCash.find(x => x.key === (Object.values(userParam).join('-')));
    if (response) return of(response);

    let params = this.GetAllHeader(userParam);

    return this.getPaginationResult(params, userParam);

  }

  private getPaginationResult(params: HttpParams, userParam: UserParams) {
    return this.httpclient.get<IUserEntity[]>(this.baseUrl + 'user', { observe: 'response', params }).pipe(
      map(response => {
        debugger;
        var obj = {
          "key": Object.values(userParam).join('-'),
          "result": response.body,
          "pagination": JSON.parse(response.headers.get('pagining') as string)
        };
        this.memoryCash.push(obj);

        this.paginationResult.result = response.body;
        if (response.headers.get('pagining') !== null) {
          this.paginationResult.pagination = JSON.parse(response.headers.get('pagining') as string);
        }
        return this.paginationResult;
      })
    );
  }


  private GetAllHeader(userParam: UserParams) {
    let params = new HttpParams();
    params = params.append('PageNumber', userParam.pageNumber.toString());
    params = params.append('PageSize', userParam.pageSize.toString());
    params = params.append('Gender', userParam.gender);
    params = params.append('minAge', userParam.minAge.toString());
    params = params.append('maxAge', userParam.maxAge.toString());
    params = params.append('orderBy', userParam.orderBy.toString());
    return params;
  }
  getUserEntityByID(Id: any) {
    // const member = this.member.find(x => x.Id === Id);
    // if (member !== undefined) {
    //   return of(member);
    // }
    debugger;
    const member = [...this.memoryCash.values()]
      .reduce((arr, elm) => arr.concat(elm.result), [])
      .find((member: IUserEntity) => member.Id == Id)
if(member) return of(member);
    return this.httpclient.get<IUserEntity>(this.baseUrl + 'user/' + Id);
  }
  userUpdatedetails(userDetail: any) {
    return this.httpclient.put(this.baseUrl + 'user/update-user', userDetail).pipe(
      map(() => {
        const index = this.member.indexOf(userDetail);
        this.member[index] = userDetail;
      })
    );
  }
  SetIsmainPhoto(PhotoID: any) {
    return this.httpclient.put(this.baseUrl + 'user/set-main-photo/' + PhotoID, PhotoID);
  }
  DeletePhoto(PhotoID: any) {
    return this.httpclient.delete(this.baseUrl + 'user/Deletephoto/' + PhotoID);
  }

  getByValue(map: any, searchValue: any) {
    for (let [key, value] of map.entries()) {
      if (key === searchValue)
        return value;
    }
  }
  // setByValue(map:any,searchValue:any, response:any) {
  //   for (let [key, value] of map.entries()) {
  //    // if (key !== searchValue)
  //              map.set(searchValue,response);
  //   }
  // }
}
