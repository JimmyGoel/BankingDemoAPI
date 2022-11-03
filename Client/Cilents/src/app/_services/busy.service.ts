import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  busyRequestCount = 0;
  constructor(private spinnerService: NgxSpinnerService) { }

  busy() {
    debugger;
    this.busyRequestCount++;
    this.spinnerService.show(undefined, {
      type: "line-scale-party",
      bdColor: "rgba(255, 255, 255, 0)",
      color: "#3333",
      template:
    "<img src='https://media.giphy.com/media/o8igknyuKs6aY/giphy.gif' />",
    })
  }
idel(){
  this.busyRequestCount--;
  if(this.busyRequestCount<=0){
    this.busyRequestCount=0;
    this.spinnerService.hide();
  }
}
}
