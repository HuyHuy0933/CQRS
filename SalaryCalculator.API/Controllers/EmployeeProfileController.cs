using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalaryCalculator.Application.Models.RequestModel;
using SalaryCalculator.Application.Models.ResponseModel;
using SalaryCalculator.Core.Framework;

namespace SalaryCalculator.API.Controllers
{
	[Authorize(Policy = "HRMSalaryPolicy")]
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeProfileController : ApiController
	{
		[HttpGet("{yearmonth}")]
		public async Task<Result<GetEmployeeProfilesResponse>> Get(string yearMonth)
		{
			var result = await Mediator.Send(new GetEmployeeProfilesRequest { YearMonth = yearMonth });

			return result;
		}
	}
}
