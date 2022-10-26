import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  users: any;
  registerModel = false;
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    //this.getUser();
  }
  registerToggle() {
    this.registerModel = true;
  }


  getUser() {

    this.http.get("http://localhost:7595/api/user").subscribe(
      response => {
        debugger;
        console.log(response);
        this.users = response
      }, error => console.log(error));
  }
cancleRegisterMode(event:boolean){
  this.registerModel=event;
}
}
