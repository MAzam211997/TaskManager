import { HttpClient, HttpEvent, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { GenericRepository } from "../GenericRepository/GenericRepository";
import { ICommonRepository } from "./ICommonRepository";
import { environment } from "../../../environments/environment";
import { Observable } from "rxjs";
import { map, catchError } from "rxjs/operators";
import { ResponseModel } from "src/app/models/sharedModels/response.model";
import { SendEmail } from "src/app/models/sharedModels/send-email.model";


@Injectable()
export class CommonRepository extends GenericRepository<any> implements ICommonRepository {

  GetTransactionType(): Observable<ResponseModel> {
    return this._http
      .get(environment.urlAddress + 'api/common/GetTransactionType', this.httpOptions)
      .pipe(map(this.extractData), catchError(this.handleError));
  }

  constructor(protected _http: HttpClient) {
    super(_http, 'api/common', environment.urlAddress);
  }

  IsValidDate(date: string, validationOption: number): Observable<any> {
    return this._http
      .get(environment.urlAddress + 'api/common/IsValidDate/' + date + '/' + validationOption, this.httpOptions)
      .pipe(map(this.extractData), catchError(this.handleError));
  }

  AbondonSession(): Observable<any> {
    return this.Get('AbondonSession');
  }

  RestartSessionTime(): Observable<any> {
    return this.Get('RestartSessionTime');
  }

  SetStringsInCache(): Observable<any> {
    return this._http
      .get(environment.urlAddress + 'api/common/SetStringsInCache', this.httpOptions).pipe(map(this.extractData), catchError(this.handleError));
  }

  // Will Use It for now onwards
  SetStringsInCacheCulturewise(cultureCode: string, refreshCache: boolean = false): Observable<any> {
    return this.Get('SetStringsInCache/' + cultureCode + '/' + refreshCache)
  }

  LoadLocalLanguages(): Observable<any> {
    return this._http
      .get(environment.urlAddress + 'api/common/LoadLocalLanguages', this.httpOptions).pipe(map(this.extractData), catchError(this.handleError));
  }

  SelectAllCurrencies(): Observable<any> {
    return this._http
      .get(environment.urlAddress + 'api/common/SelectAllCurrencies', this.httpOptions).pipe(map(this.extractData), catchError(this.handleError));
  }

  LoadAllLocalLanguages(): Observable<any> {
    return this._http
      .get(environment.urlAddress + 'api/common/LoadAllLocalLanguages', this.httpOptions).pipe(map(this.extractData), catchError(this.handleError));
  }

  LoadLocalLanguagesWithTableValue(Table: string, CodeColumn: string, CodeValue?: string, IsInserting?: boolean, CheckFiscalSpanID?: boolean): Observable<any> {
    return this._http
      .get(environment.urlAddress + 'api/common/LoadLocalLanguagesWithTableValue/' + Table + '/' + CodeColumn + '/' + CodeValue + '/' + IsInserting + "/" + CheckFiscalSpanID, this.httpOptions).pipe(map(this.extractData), catchError(this.handleError));
  }

  SetSessionVariables(string: string, value: string): Observable<any> {
    return this.Get('SetSessionVariables/' + string + '/' + value);
  }

  IsFieldValueDuplicated(table: string, column: string, value: string): Observable<any> {
    return this._http
      .get(environment.urlAddress + 'api/common/IsFieldValueDuplicated/' + table + '/' + column + '/' + value, this.httpOptions).pipe(map(this.extractData), catchError(this.handleError));
  }

  IsDuplicated(table: string, column: string, value: string, primaryColumn: string, primaryColumnValue: string): Observable<any> {
    return this._http
      .get(environment.urlAddress + 'api/common/IsDuplicated/' + table + '/' + column + '/' + value + '/' + primaryColumn + '/' + primaryColumnValue, this.httpOptions).pipe(map(this.extractData), catchError(this.handleError));
  }

  public ConvertDateToSaveFormat(dates: string): Observable<ResponseModel> {

    while (dates.includes('/')) {
      dates = dates.replace('/', '-')
    }

    return this._http
      .get(environment.urlAddress + 'api/common/ConvertDateToSaveFormat/' + dates, this.httpOptions).pipe(map(this.extractData), catchError(this.handleError));
  }

  CachedValueCheck(actionName: string, key: string = ""): Observable<ResponseModel> {
    return this.GetById(actionName + '/' + key)
  }

  GetTimeZones(): Observable<ResponseModel> {
    return this._http
      .get(environment.urlAddress + 'api/common/GetTimeZones/', this.httpOptions).pipe(map(this.extractData), catchError(this.handleError));
  }

  GetDates(month: string, year: string): Observable<ResponseModel> {
    return this.GetById('GetDates/' + month + '/' + year)
  }

  GetCurrentLoggedInUser_UserId() {
    return parseInt(localStorage.getItem('UserID'));
  }

  GetCurrentLoggedInUser_RoleID() {
    return parseInt(localStorage.getItem('userRoleID'));
  }

  GetCurrentLoggedInUser_IsSigningPartner() {
    return JSON.parse(localStorage.getItem('isSigningPartner'));
  }
  SendBodyDataInMail(sendEmail: SendEmail): Observable<any> {
    return this.PostData('/SendEmailToUser', sendEmail).pipe(map(this.extractData), catchError(this.handleError));
  }
  //#endregion
}

