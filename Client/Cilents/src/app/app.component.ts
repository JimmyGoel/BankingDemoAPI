import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Banking App';
  users: any;
  constructor(private http: HttpClient) {
  }
  ngOnInit(): void {
    this.getUser();
  }

  getUser() {
    // this.http.get("http://localhost:7595/api/user").subscribe(
    // {
    //     next: response => this.users = response,
    //     error: error => console.log(error),
    //   })
    this.http.get("http://localhost:7595/api/user").subscribe(
      response => {
        debugger;
        console.log(response);
        this.users = response
      }, error => console.log(error));
  }
}
