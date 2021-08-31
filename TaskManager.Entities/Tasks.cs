using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Entities
{
	public class Tasks : AuditableEntity
	{
		public int TaskId { get; set; }
		public string TaskTitle { get; set; }
		public string TaskDescription { get; set; }
	}
}
