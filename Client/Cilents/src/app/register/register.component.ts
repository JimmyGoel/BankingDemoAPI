import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { response } from 'express';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  // @Input() UserFromHomeComponent: any;
  @Output() cancleRegister = new EventEmitter();
  model: any = {}
  constructor(private AccountService: AccountService,private toastr:ToastrService) { }

  ngOnInit(): void {
  }
  registerModel() {
    console.log(this.model);
    this.AccountService.register(this.model).subscribe((response) => {
      console.log(response);
      this.cancel();
    }, error => {
      console.log(error);
      this.toastr.error(error);
    })
  }
  cancel() {
    this.cancleRegister.emit(false);
  }

}
