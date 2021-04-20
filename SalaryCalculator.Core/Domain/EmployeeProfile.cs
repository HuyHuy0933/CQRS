using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculator.Core.Domain
{
	public class EmployeeProfile
	{
		public EmployeeProfile(
			string email,
			string fullname,
			string employeeType,
			string position,
			DateTime[] dependantEndDates,
			bool inUnion,
			DateTime? terminatedDate,
			bool isForeigner)
		{
			Email = email;
			Fullname = fullname;
			EmployeeType = employeeType;
			Position = position;
			DependantEndDates = dependantEndDates;
			InUnion = inUnion;
			TerminatedDate = terminatedDate;
			IsForeigner = isForeigner;
		}
		public string Email { get; set; }
		public string Fullname { get; set; }
		public string EmployeeType { get; set; }
		public string Position { get; set; }
		public DateTime[] DependantEndDates { get; set; }
		public bool InUnion { get; set; }
		public DateTime? TerminatedDate { get; set; }
		public bool IsForeigner { get; set; }
	}
}
