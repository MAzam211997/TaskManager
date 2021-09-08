export class auditableEntity
{
  CreatedDate?: Date = new Date();
  ModifiedDate?: Date = new Date();
  DeletedDate?: Date = new Date();
  CreatedBy?: number = 0;
  ModifiedBy?: number = 0;
  DeletedBy?: number = 0;

  Total?: number = 0;
  RowIndex?: number = 0;
  IsActive?: boolean = false;
  IsInserting?: boolean = false;
  IsDeleted?: boolean = false;
}
