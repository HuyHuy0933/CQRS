using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculator.Application.Models.ResponseModel
{
	public class GetEmployeeProfilesResponse
	{
		public GetEmployeeProfilesResponse(
			EmployeeProfilesResponse[] profiles)
		{
			Profiles = profiles;
		}
		public EmployeeProfilesResponse[] Profiles { get; }
	}

	public class EmployeeProfilesResponse
	{
		public EmployeeProfilesResponse(
			string email,
			string fullname)
		{
			Email = email;
			Fullname = fullname;
		}
		public string Email { get; }
		public string Fullname { get; }
	}
}
