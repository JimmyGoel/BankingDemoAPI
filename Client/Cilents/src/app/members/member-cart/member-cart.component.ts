import { Component, Input, OnInit } from '@angular/core';
import { IUserEntity } from 'src/app/_modle/IuserEntity';

@Component({
  selector: 'app-member-cart',
  templateUrl: './member-cart.component.html',
  styleUrls: ['./member-cart.component.css']
})
export class MemberCartComponent implements OnInit {
  @Input() member!:IUserEntity;
  constructor() { }

  ngOnInit(): void {
  }

}
