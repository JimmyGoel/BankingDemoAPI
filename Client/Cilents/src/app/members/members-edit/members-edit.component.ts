import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { IUserEntity } from 'src/app/_Interface/IuserEntity';
import { IUserDetail } from 'src/app/_Interface/user';
import { AccountService } from 'src/app/_services/account.service';
import { UserentityService } from 'src/app/_services/userentity.service';

@Component({
  selector: 'app-members-edit',
  templateUrl: './members-edit.component.html',
  styleUrls: ['./members-edit.component.css']
})
export class MembersEditComponent implements OnInit {
  @ViewChild('EditForm') EditForm:NgForm;
  member: IUserDetail;
  userEntity: IUserEntity;
@HostListener('window:beforeunload',['$event']) unloadNotification($event:any){
  if(this.EditForm.dirty){
    return $event.returnValue= true;
  }
}


  constructor(private accoutService: AccountService, 
    private memberService: UserentityService, private Toastservice:ToastrService) {
    this.accoutService.currentUser$.pipe(take(1)).subscribe((membera: IUserDetail) => {
      this.member = membera;
    })
  }

  ngOnInit(): void {
    this.loadUser();
  }
  loadUser() {
    this.memberService.getUserEntityByID(this.member.id).subscribe(resp => {
      this.userEntity = resp;
    })
  }

  updateDataMember(){
    console.log(this.userEntity);
    this.memberService.userUpdatedetails(this.userEntity).subscribe(resp=>{
      this.Toastservice.success("Sucess Successfully");
      this.EditForm.reset(this.userEntity);
    })
  
  }
}
