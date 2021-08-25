import { Observable } from "rxjs";

export interface IGenericRepository<T> {

  httpOptions: any;
  Post(item: T): Observable<T>;
  Put(item: T): Observable<T>;
  GetById(code: string): Observable<T>;


  Get(tableNames: string): Observable<T>; // need to remove Duplication
  Delete(code: string): Observable<T>;
  GetList(subUrl: any): Observable<T[]>;
  PostFile(actionName: string, formData: FormData): Observable<T>;
  handleError(error: any);
}
