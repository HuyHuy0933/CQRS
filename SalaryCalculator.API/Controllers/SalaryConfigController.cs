using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SalaryCalculator.Application.Models.RequestModel;
using SalaryCalculator.Application.Models.ResponseModel;
using SalaryCalculator.Core.Domain;
using SalaryCalculator.Core.Framework;

namespace SalaryCalculator.API.Controllers
{
	[Authorize(Policy = "HRMSalaryPolicy")]
	[Route("api/[controller]")]
	[ApiController]
	public class SalaryConfigController : ApiController
	{
		private readonly ILogger<SalaryConfigController> _logger;

		public SalaryConfigController(
			ILogger<SalaryConfigController> logger)
		{
			_logger = logger;
		}

		[HttpGet()]
		public async Task<Result<GetSalaryConfigResponse>> GetSalaryConfig()
		{
			var result = await Mediator.Send(new GetSalaryConfigRequest());

			return result;
		}

		[HttpPost()]
		public async Task<Result<SaveSalaryConfigResponse>> SaveSalaryConfig(SaveSalaryConfigRequest request)
		{
			if(request == null)
			{
				_logger.LogError(Errors.SalaryConfig.SaveSalaryConfigRequestModelNullError.Message);
				return Errors.SalaryConfig.SaveSalaryConfigRequestModelNullError;
			}
			var result = await Mediator.Send(request);
			return result;
		}
	}
}
