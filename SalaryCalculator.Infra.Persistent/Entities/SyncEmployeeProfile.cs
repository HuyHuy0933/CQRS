using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculator.Infra.Persistent.Entities
{
	public class SyncEmployeeProfile : EntityBase
	{
		public string Email { get; set; }
		public string FullName { get; set; }
		public string EmployeeType { get; set; }
		public string Position { get; set; }
		public string DependantEndDates { get; set; }
		public bool InUnion { get; set; }
		public DateTime? TerminatedDate { get; set; }
		public bool IsForeigner { get; set; }
	}
}
