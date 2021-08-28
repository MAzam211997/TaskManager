import { IGenericRepository } from "../GenericRepository/IGenericRepository";
import { Observable } from "rxjs";
import { Users } from "../../models/Users/users.model";

export interface IUsersRepository extends IGenericRepository<Users> {
  GetAll(currentPage: number, recordsPerPage: number, filterText: string): Observable<any>;
  DeleteRec(userID: string, fileName: string): Observable<any>;
  UpdatePassword(user: Users): Observable<any>;
  GetUserByID(userID: string): Observable<any>;
  LogoutForcefully(user: Users): Observable<any>;
}
