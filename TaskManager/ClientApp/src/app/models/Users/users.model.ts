import { auditableEntity } from "../auditableEntity/auditableEntity.model";

export class Users extends auditableEntity
{
  UserID?: number=0;
  RoleID: number=0;
  RoleName: string='';
  Initial: string='';
  FullName: string='';
  EmailAddress: string='';
  Mobile: string='';
  Password: string = '';
  CurrentPassword: string = '';
  OldPassword: string='';
  NewPassword: string='';
  ConfirmNewPassword: string='';
  IsSigningPartner: boolean = false;
  Signature: string = '';
  ArabicName: string = '';
  Sign_File: File;
  OldFile: string = '';
}
