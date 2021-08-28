import { ILoginRepository } from "./ILoginRepository";
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { GenericRepository } from "../GenericRepository/GenericRepository";
import { Login } from "../../models/Login/Login.model";
import { environment } from "../../../environments/environment";
import { catchError, map } from "rxjs/operators";
import { Observable } from "rxjs";
import { ResponseModel } from "../../models/SharedModels/response.model";

@Injectable()
export class LoginRepository extends GenericRepository<Login> implements ILoginRepository {
  url: string = environment.urlAddress;
  constructor(protected _http: HttpClient) {
    super(_http, 'api/account', environment.urlAddress);
  }

  Authentication(login: Login) {
    return this.Post(login);
  }

  AuthenticateForGotPasswordUser(login: Login): Observable<ResponseModel> {
    //return this.Post(login);
    return this.PostData('/AuthenticateForGotPasswordUser', login).pipe(map(this.extractData), catchError(this.handleError));
  }

  checkSession() {
    return this._http.get(this.url + 'api/common/CheckSession', this.httpOptions).pipe(map(this.extractData), catchError(this.handleError));
  }
}
