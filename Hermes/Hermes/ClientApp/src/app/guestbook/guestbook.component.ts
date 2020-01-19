import { Component, OnInit } from '@angular/core';
import { AuthorizeService } from "../../api-authorization/authorize.service";
import * as signalR from "@microsoft/signalr";
import { GuestbookService } from '../service/guestbook.service';

import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-guestbook',
  templateUrl: './guestbook.component.html',
  styleUrls: ['./guestbook.component.css']
})
export class GuestbookComponent implements OnInit {
  user: any;
  comments: any[];
  noteToUpdate: string;
  commentForm = new FormGroup({
    content: new FormControl('', [Validators.required]),
  });

  constructor(private authorizeService: AuthorizeService, private guestbookService: GuestbookService) {

  }
  ngOnInit() {
    this.noteToUpdate = "";
    //Load comments
    this.loadComments();

    //Get current user
    this.authorizeService.getUser().subscribe(u => {
      this.user = u;
    });

    const connection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Information)
      .withUrl("notify")
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
    connection.on("BroadcastUpdate", () => {
      this.loadComments();
    });
    connection.on("BroadcastDelete", () => {
      this.loadComments();
    });

  }

  loadComments() {
    this.guestbookService.getComments().subscribe(c => {
      this.comments = c;
    });
  }

  //Post comment
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

  //Reset commetForm
  revert() {
    this.commentForm.reset();
  }

  postLike(commentId: number, likes: any[]) {
    var id = undefined;
    if (likes.length > 0) {
      //check if user has already liked the post
      var like = likes.find(l => l.commentId === commentId && l.userId === this.user.sub);
      if (like !== undefined) {
        id = like.id;
      }
    }
    let data = {
      id: id,
      commentId: commentId,
      userId: this.user.sub,
    };
    this.guestbookService.postLike(data).subscribe(r => {
      console.log("Like added");
    });
  }

  deleteComment(modal: any, comment: any) {
    this.guestbookService.deleteComment(comment.id).subscribe(c => {
      console.log("Comment deleted");
      modal.hide();
    });
  }

  toggleUpdateCommentModal(modal: any, comment: any, open: boolean) {
    this.noteToUpdate = comment.note;
    if (open) {
      modal.show();
    } else {
      modal.hide();
    }
  }

  updateComment(modal: any, comment: any) {
    comment.note = this.noteToUpdate;
    this.guestbookService.updateComment(comment.id, comment).subscribe(c => {
      console.log("comment updated");
      modal.hide();
    });
  }

  userHasLiked(comment: any) {
    console.log("comment");
    return true;
  }
}
