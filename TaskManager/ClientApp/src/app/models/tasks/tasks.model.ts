import { auditableEntity } from "../auditableEntity/auditableEntity.model";

export class tasks extends auditableEntity
{
  TaskId: number=0;
  TaskTitle: string='';
  TaskDescription: string='';
  IsCompleted: boolean = false;
  CompletionDate?: Date = new Date();
  CompletedBy?: number = 0;
}
