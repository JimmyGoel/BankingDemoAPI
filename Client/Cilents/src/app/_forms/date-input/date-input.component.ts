import { Component, Input, OnInit, Self } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';

@Component({
  selector: 'app-date-input',
  templateUrl: './date-input.component.html',
  styleUrls: ['./date-input.component.css']
})
export class DateInputComponent implements ControlValueAccessor {
 @Input() label:string;
 @Input() maxDate:Date;

 bsconfig:Partial<BsDatepickerConfig>

  constructor(@Self() public ngcontrol:NgControl) {
    this.ngcontrol.valueAccessor=this;
    this.bsconfig={
      containerClass:'theme-red',
      dateInputFormat:'DD MMMM YYYY'
    }
   }
  writeValue(obj: any): void {
   
  }
  registerOnChange(fn: any): void {
    
  }
  registerOnTouched(fn: any): void {
   
  }
 

}
