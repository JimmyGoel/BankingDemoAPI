import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, FormRecord, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
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
  registerForm: FormGroup;
  maxDate:Date;
  ValidationError:string[]=[];
  constructor(private AccountService: AccountService, 
    private toastr: ToastrService, private fb:FormBuilder, private router:Router) { }

  ngOnInit(): void {
    this.initinalizeForm();
    this.maxDate=new Date();
    this.maxDate.setFullYear(this.maxDate.getFullYear() -18);
  }

  initinalizeForm() {
    this.registerForm = this.fb.group({
      gender: ['male'],
      username: ['', Validators.required],
      knownAs: ['', Validators.required],
      DateofBirth: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      password: ['', [Validators.required,
      Validators.minLength(4), Validators.maxLength(8)]],
      confirmPassword: ['', [Validators.required,this.matchValue('password')]]
    })
    this.registerForm.controls['password'].valueChanges.subscribe(()=>{
      this.registerForm.controls['confirmPassword'].updateValueAndValidity();
    })
  }

  matchValue(matchTo:string): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value === (control?.parent?.controls as { [key: string]: AbstractControl })[matchTo].value ? null : { isMatching: true };
    }
  }

  registerModel() {

    console.log(this.registerForm.value);
    this.AccountService.register(this.registerForm.value).subscribe((response) => {
      console.log(response);
      //this.cancel();
      this.router.navigateByUrl('/members')
    }, error => {
      console.log(error);
      this.ValidationError=error;
      //this.toastr.error(error);
    })
    
  }
  cancel() {
    this.cancleRegister.emit(false);
  }

}
