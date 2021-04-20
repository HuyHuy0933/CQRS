using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalaryCalculator.API.Common.Policies
{
	public class HRMSalaryRequirement : IAuthorizationRequirement
	{
		public HRMSalaryRequirement(
			string userRole)
		{
			UserRole = userRole;
		}
		public string UserRole { get; }
	}
}
