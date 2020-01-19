import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http';
import { retry, catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GuestbookService {
  private baseurlComment = 'api/Comment';
  private baseurlLike = 'api/Like';
  constructor(private http: HttpClient) {
  }

  // Http Headers
  httpOptions = {

    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
    })
  }

  getComments(): Observable<any[]> {
    return this.http.get<any[]>(this.baseurlComment)
      .pipe(
        retry(1),
        catchError(this.errorHandl)
      );
  }


  postComment(comment): Observable<any> {
    return this.http.post<any>(this.baseurlComment, JSON.stringify(comment), this.httpOptions)
      .pipe(
        retry(1),
        catchError(this.errorHandl)
      );
  }

  postLike(like): Observable<any> {
    return this.http.post<any>(this.baseurlLike, JSON.stringify(like), this.httpOptions)
      .pipe(
        retry(1),
        catchError(this.errorHandl)
      );
  }

  deleteComment(id: number): Observable<any> {
    return this.http.delete<any>(this.baseurlComment + '/' + id, this.httpOptions)
      .pipe(
        retry(1),
        catchError(this.errorHandl)
      );
  }


  updateComment(id: number, comment): Observable<any> {
    return this.http.put<any>(this.baseurlComment + '/' + id, JSON.stringify(comment), this.httpOptions)
      .pipe(
        retry(1),
        catchError(this.errorHandl)
      );
  }

  // Error handling
  errorHandl(error) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  }
}
