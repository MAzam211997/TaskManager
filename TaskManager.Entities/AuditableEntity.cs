using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Entities
{
    public class AuditableEntity
    {
        public DateTime? CreatedDate{ get; set; }
        public DateTime? ModifiedDate{ get; set; }
        public DateTime? DeletedDate{ get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public int? DeletedBy { get; set; }
        public bool IsActive { set; get; }
        public bool IsInserting { set; get; }
        public int? Total { set; get; }
        public int? RowIndex { set; get; }
        public int? IsDeleted { get; set; }
    }
}
