import { Component, OnInit } from '@angular/core';
import { AuthorizeService } from "../../api-authorization/authorize.service";

import * as signalR from "@microsoft/signalr";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  test: any;

  constructor(private authorizeService: AuthorizeService, ) {

  }

  //this.hubConnection = new signalR.HubConnectionBuilder()
  // .withUrl("/hub")
  // .build();

  ngOnInit() {
    this.authorizeService.getUser().subscribe(x => {
      this.test = x;
    });

    const connection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Information)
      .withUrl("/hub")
      .build();

    connection.start().then(() => {
      console.log('SignalR Connected!');
    }).catch(err => console.error(err.toString()));

    connection.on("BroadcastMessage", () => {

    }); 
  }
}
