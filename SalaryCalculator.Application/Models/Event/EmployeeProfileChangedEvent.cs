using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculator.Application.Models.Event
{
	public class EmployeeProfileChangedEvent
	{
		public string Email { get; set; }
		public string Fullname { get; set; }
		public string EmployeeType{get;set;}
		public string Position { get; set; }
		public bool IsForeigner { get; set; }
		public long[] DependantEndDates { get; set; }
		public bool InUnion { get; set; }
		public long TerminationDate { get; set; }
		public long SendDate { get; set; }
	}
}
