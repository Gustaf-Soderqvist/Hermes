import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GuestbookCommentComponent } from './guestbook-comment.component';

describe('GuestbookCommentComponent', () => {
  let component: GuestbookCommentComponent;
  let fixture: ComponentFixture<GuestbookCommentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GuestbookCommentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GuestbookCommentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
