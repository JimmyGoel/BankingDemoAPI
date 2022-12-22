import { Component, OnInit } from '@angular/core';
import { Observable, take } from 'rxjs';
import { IPagination } from 'src/app/_Interface/Ipagination';
import { IUserEntity } from 'src/app/_Interface/IuserEntity';
import { IUserDetail } from 'src/app/_Interface/user';
import { UserParams } from 'src/app/_Interface/userParams';
import { AccountService } from 'src/app/_services/account.service';
import { UserentityService } from 'src/app/_services/userentity.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

  //userEntity$: Observable< IUserEntity[]>;
  userEntity: IUserEntity[];
  pagination: IPagination | undefined;
  userParams: UserParams
  user: IUserDetail
  genderList=[{value:'Male',display:'Males'},{value:'Female',display:'Females'}]

  constructor(private userService: UserentityService, private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(resp => {
      this.user = resp;
debugger;
      this.userParams = new UserParams(this.user);
    })
  }

  ngOnInit(): void {
    //this.userEntity$ = this.userService.getUserEntity();
    this.loadMember();
    
  }
  // getUserEntity(){
  //   this.userService.getUserEntity().subscribe((resp) => {
  //     this.userEntity=resp;
  //   });
  // }
  loadMember() {
    this.userService.getUserEntity(this.userParams).subscribe(response => {
      debugger;
      this.userEntity = response.result ;
      this.pagination = response.pagination;
    })
  }
  pageChanged(event: any) {
    this.userParams.pageNumber = event.page;
    this.loadMember();
  }
  resetFilter(){
    this.userParams=new UserParams(this.user);
    this.loadMember();
  }
}
