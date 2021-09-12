import { GenericRepository } from "../GenericRepository/GenericRepository";
import { Observable } from "rxjs";
import { catchError, map } from "rxjs/operators";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ITasksRepository } from "./ITasksRepository";
import { environment } from "../../../environments/environment";
import { tasks } from "../../models/tasks/tasks.model";

@Injectable()
export class TasksRepository extends GenericRepository<tasks> implements ITasksRepository
{

  url: string = environment.urlAddress;
  constructor(protected _http: HttpClient) {
    super(_http, 'api/Tasks/', environment.urlAddress);
  }

  GetAll(currentPage: number, recordsPerPage: number, filterText: string): Observable<any> {
    filterText = filterText.split('/').join('~');
    if (filterText == "") {
      filterText = "|";
    }
    return this._http
      .get(this.url + 'api/Tasks/SelectAll' + '/' + currentPage + '/' + recordsPerPage + '/' + filterText, this.httpOptions).pipe(map(this.extractData), catchError(this.handleError));
  }

  DeleteRec(taskID: string) {
    return this._http
      .delete('api/Tasks/DeleteRec' + '/' + taskID , this.httpOptions).pipe(map(this.extractData), catchError(this.handleError));

  }

  SaveRec(task: tasks): Observable<any> {
    var formData: FormData = new FormData();
    return this.PostFile('SaveRec', formData).pipe(map(this.extractData), catchError(this.handleError));
  }

  GetTaskByID(taskID: string): Observable<any> {
    return this._http
      .get(this.url + 'api/Tasks/LoadRec/' + taskID, this.httpOptions).pipe(map(this.extractData), catchError(this.handleError));
  }
}
