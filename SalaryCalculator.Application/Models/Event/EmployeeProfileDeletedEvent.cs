using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculator.Application.Models.Event
{
	public class EmployeeProfileDeletedEvent
	{
		public string Email { get; set; }
		public long SendDate { get; set; }
	}
}
