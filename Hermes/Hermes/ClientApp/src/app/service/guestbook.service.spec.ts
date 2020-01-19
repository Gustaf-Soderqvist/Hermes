import { TestBed } from '@angular/core/testing';

import { GuestbookService } from './guestbook.service';

describe('GuestbookService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: GuestbookService = TestBed.get(GuestbookService);
    expect(service).toBeTruthy();
  });
});
