import { IGenericRepository } from "../GenericRepository/IGenericRepository";
import { Observable } from "rxjs";
import { HttpEvent } from "@angular/common/http";
import { SendEmail } from "src/app/models/sharedModels/send-email.model";
export interface ICommonRepository extends IGenericRepository<any> {
  SetStringsInCache(): any;
  LoadLocalLanguages(isCultureCode: boolean): Observable<any>;
  LoadLocalLanguagesWithTableValue(Table: string, CodeColumn: string, CodeValue: string, IsInserting: boolean, CheckFiscalSpanID: boolean): Observable<any>;
  SetSessionVariables(code: string, value: string): Observable<any>;
  IsValidDate(date: string, validationOption: number): Observable<any>;
  IsFieldValueDuplicated(table: string, column: string, value: string): Observable<any>;
  RestartSessionTime(): Observable<any>
  AbondonSession(): Observable<any>
  GetCurrentLoggedInUser_UserId();
  GetCurrentLoggedInUser_RoleID();
  GetCurrentLoggedInUser_IsSigningPartner();
  SendBodyDataInMail(sendEmail: SendEmail): Observable<any>;
}
