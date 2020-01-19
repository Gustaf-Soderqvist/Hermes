import { Component, OnInit } from '@angular/core';
import { AuthorizeService } from "../../api-authorization/authorize.service";
import * as signalR from "@microsoft/signalr";
import { GuestbookService } from '../service/guestbook.service';
import { Observable } from 'rxjs';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-guestbook',
  templateUrl: './guestbook.component.html',
  styleUrls: ['./guestbook.component.css']
})
export class GuestbookComponent implements OnInit {
  user: any;
  restaurants$: Observable<any[]>;
  comments: any;

  commentForm = new FormGroup({
    content: new FormControl('', [Validators.required]),
  });

  constructor(private authorizeService: AuthorizeService, private guestbookService: GuestbookService) {

  }
  ngOnInit() {
    //Load comments
    this.loadComments();

    //Get current user
    this.authorizeService.getUser().subscribe(u => {
      this.user = u;
    });

    const connection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Information)
      .withUrl("https://localhost:44355/notify")
      .build();

    connection.start().then(() => {
      console.log('SignalR Connected!');
    }).catch(err => console.error(err.toString()));

    connection.on("BroadcastMessage", () => {
      this.loadComments();
    });
    connection.on("BroadcastLike", () => {
      this.loadComments();
    });

  }

  loadComments() {
    this.guestbookService.getComments().subscribe(c => {
      this.comments = c;
    });
  }

  //Remove this later
  addComment() {

    let note = {
      note: "test test test",
      createdDate: new Date(),
      userId: this.user.sub,
    }
    this.guestbookService.postComment(note).subscribe(r => {
      console.log("Comment added");
    });
    console.log("test");
  }

  onSubmit() {
    if (!this.commentForm.valid) {
      return;
    }

    let note = {
      note: this.commentForm.get('content').value,
      createdDate: new Date(),
      userId: this.user.sub,

    };

    this.guestbookService.postComment(note).subscribe(r => {
      this.revert();
      console.log("Comment added");
    });
  }

  revert() {
    this.commentForm.reset();
  }

  postLike(commentId: number) {
    let like = {
      commentId: commentId,
      userId: this.user.sub,
    };
    this.guestbookService.postLike(like).subscribe(r => {
      console.log("Like added");
    });
  }
}
