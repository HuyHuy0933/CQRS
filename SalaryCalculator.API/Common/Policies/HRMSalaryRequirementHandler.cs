using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalaryCalculator.API.Common.Policies
{
	public class HRMSalaryRequirementHandler : AuthorizationHandler<HRMSalaryRequirement>
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
			HRMSalaryRequirement requirement)
		{
			var userRole = context.User?.FindFirst("user_role")?.Value;
			if (string.IsNullOrWhiteSpace(userRole) is false && userRole.Contains(requirement.UserRole)) 
				context.Succeed(requirement);
			return Task.CompletedTask;
		}
	}
}
