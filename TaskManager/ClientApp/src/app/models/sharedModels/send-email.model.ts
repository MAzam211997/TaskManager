
export class SendEmail {
  EmailAddress: string = '';
  RecordNumber: string = '';
  EmailSubject: string = '';
  EmailBody: string = '';
  UserName: string = '';
  HasAttachment: boolean = false;
  EmailType: EmailType;
}

export enum EmailType {
  Default,
  Reminder,
  Notify
}
