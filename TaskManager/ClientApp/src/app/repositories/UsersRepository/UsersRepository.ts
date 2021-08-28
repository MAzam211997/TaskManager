import { GenericRepository } from "../GenericRepository/GenericRepository";
import { environment } from "src/environments/environment";
import { Observable } from "rxjs";
import { catchError, map } from "rxjs/operators";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Users } from "../../models/Users/users.model";
import { IUsersRepository } from "./IUsersRepository";
import { ResponseModel } from "../../models/SharedModels/response.model";

@Injectable()
export class UsersRepository extends GenericRepository<Users> implements IUsersRepository {

  url: string = environment.urlAddress;
  constructor(protected _http: HttpClient) {
    super(_http, 'api/Users/', environment.urlAddress);
  }

  GetAll(currentPage: number, recordsPerPage: number, filterText: string): Observable<any> {
    filterText = filterText.split('/').join('~');
    if (filterText == "") {
      filterText = "|";
    }
    return this._http
      .get(this.url + 'api/Users/SelectAll' + '/' + currentPage + '/' + recordsPerPage + '/' + filterText, this.httpOptions).pipe(map(this.extractData), catchError(this.handleError));
  }


  SelectUsers(username: string): Observable<any> {
    debugger
    return this._http
      .get(this.url + 'api/Deliverables/SelectUsers/' + username, this.httpOptions).pipe(map(this.extractData), catchError(this.handleError));
  }
  DeleteRec(userID: string, fileName: string) {
    return this._http
      .delete('api/Users/DeleteRec' + '/' + userID + "/" + fileName, this.httpOptions).pipe(map(this.extractData), catchError(this.handleError));

  }

  SaveRec(item: Users): Observable<any> {
    var formData: FormData = new FormData();

    if (item.Sign_File) {
      formData.append('file', item.Sign_File, item.Sign_File.name);
      formData.append('Name', item.Sign_File.name);
    } else {
      formData.append('Name', item.Signature);
    }

    formData.append('UserID', item.UserID.toString());
    formData.append('FullName', item.FullName);
    formData.append('EmailAddress', item.EmailAddress);
    formData.append('Initial', item.Initial);
    formData.append('RoleID', item.RoleID.toString());
    formData.append('Mobile', item.Mobile);
    formData.append('Password', item.Password);
    formData.append('CurrentPassword', item.CurrentPassword);
    formData.append('IsActive', item.IsActive.toString());
    formData.append('IsInserting', item.IsInserting.toString());
    formData.append('IsSigningPartner', item.IsSigningPartner.toString());
    formData.append('Signature', item.Signature);
    formData.append('OldFile', item.OldFile);
    formData.append('ArabicName', item.ArabicName);

    return this.PostFile('SaveRec', formData).pipe(map(this.extractData), catchError(this.handleError));
  }

  UpdateProfile(item: Users): Observable<any> {
    return this.PostData('UpdateProfile', item).pipe(map(this.extractData), catchError(this.handleError));
  }

  UpdatePassword(user: Users): Observable<ResponseModel> {
    debugger
    return this.PostData('ChangePassword/', user).pipe(map(this.extractData), catchError(this.handleError));
  }

  GetUserByID(userID: string): Observable<any> {
    return this._http
      .get(this.url + 'api/Users/LoadRec/' + userID, this.httpOptions).pipe(map(this.extractData), catchError(this.handleError));
  }
  LogoutForcefully(user: Users): Observable<ResponseModel> {
    return this.PostData('/LogoutForcefully', user).pipe(map(this.extractData), catchError(this.handleError));
  }
}
