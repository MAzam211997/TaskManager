import { IGenericRepository } from "../GenericRepository/IGenericRepository";
import { Observable } from "rxjs";
import { tasks } from "../../models/tasks/tasks.model";

export interface ITasksRepository extends IGenericRepository<tasks> {
  GetAll(currentPage: number, recordsPerPage: number, filterText: string): Observable<any>;
  DeleteRec(taskID: string, fileName: string): Observable<any>;
  GetTaskByID(taskID: string): Observable<any>;
  SaveRec(task: tasks): Observable<any>;
}
