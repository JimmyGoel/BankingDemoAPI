import { Component, OnInit } from '@angular/core';
import { IUserEntity } from 'src/app/_modle/IuserEntity';
import { UserentityService } from 'src/app/_services/userentity.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

   userEntity:IUserEntity[] | undefined;
  constructor(private userService:UserentityService) { }

  ngOnInit(): void {
    this.getUserEntity();
  }
  getUserEntity(){
    this.userService.getUserEntity().subscribe((resp) => {
      this.userEntity=resp;
    });
  }

}
