import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Observable, of, throwError } from "rxjs";
import { IGenericRepository } from "./IGenericRepository";
import { map, catchError } from "rxjs/operators";

export abstract class GenericRepository<T> implements IGenericRepository<T> {
  httpOptions: any = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private httpClient: HttpClient,
    private endpoint: string,
    private endUrl: string) {
  }

  Post(item: T): Observable<T> {
    return this.httpClient.post(this.endUrl + this.endpoint, item, this.httpOptions)
      .pipe(map(this.extractData), catchError(this.handleError));
  }

  PostData(actionName: string, item: T): Observable<T> {
    return this.httpClient.post(this.endUrl + this.endpoint + actionName, item, this.httpOptions)
      .pipe(map(this.extractData), catchError(this.handleError));
  }

  PostFile(actionName: string, formData: FormData): Observable<T> {
    return this.httpClient.post(this.endUrl + this.endpoint + actionName, formData)
      .pipe(map(this.extractData), catchError(this.handleError));
  }

  Put(item: T): Observable<T> {
    return this.httpClient
      .put<T>(this.endUrl + this.endpoint, item, this.httpOptions)
      .pipe(map(this.extractData), catchError(this.handleError));
  }

  GetById(code: string): Observable<T> {
    return this.httpClient
      .get(this.endUrl + this.endpoint + '/' + code, this.httpOptions)
      .pipe(map(this.extractData), catchError(this.handleError));
  }

  Delete(code: string): Observable<T> {
    return this.httpClient
      .delete(this.endUrl + this.endpoint + code, this.httpOptions)
      .pipe(map(this.extractData), catchError(this.handleError));
  }

  Get(tableNames: string): Observable<T> {
    return this.httpClient.get(this.endUrl + this.endpoint + '/' + tableNames, this.httpOptions).pipe(
      map(this.extractData),
      catchError(this.handleError));
  }
  autoSearch(table: string): Observable<T> {
    return this.httpClient.get(this.endUrl + this.endpoint + '/AutoSearch/' + '/' + table, this.httpOptions).pipe(
      map(this.extractData),
      catchError(this.handleError));
  }
  extractData(res: any) {
    let body = res;
    return body || {};
  }

  GetList(subUrl: any): Observable<T[]> {
    return this.httpClient.get<T[]>(this.endUrl + subUrl);
  }

  handleError(error) {
    if (error.status && typeof (error.error) == "object") {
      if (error.error._statusCode === 401) {
        localStorage.clear();
        localStorage.setItem('IsLoggingOut', "true");
        window.location.href = "/Login";
      }
    } else {
      let errorMessage = '';
      if (error.error instanceof ErrorEvent) {
        // client-side error
        errorMessage = 'Error: ' + error.error.message;
      } else {
        // server-side error
        errorMessage = 'Error Code: ' + error.status;
      }
      window.alert(errorMessage);
      return of(errorMessage);
    }
  }
}
