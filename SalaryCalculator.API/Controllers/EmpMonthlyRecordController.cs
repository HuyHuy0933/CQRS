using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SalaryCalculator.Application.Models.RequestModel;
using SalaryCalculator.Application.Models.ResponseModel;
using SalaryCalculator.Core.Domain;
using SalaryCalculator.Core.Framework;
using SalaryCalculator.Core.Repository;

namespace SalaryCalculator.API.Controllers
{
	[Authorize(Policy = "HRMSalaryPolicy")]
	[Route("api/[controller]")]
	[ApiController]
	public class EmpMonthlyRecordController : ApiController
	{
		private readonly ILogger<EmpMonthlyRecordController> _logger;

		public EmpMonthlyRecordController(
			ILogger<EmpMonthlyRecordController> logger)
		{
			_logger = logger;
		}

		[HttpGet("{key}/{yearMonth}")]
		public async Task<Result<GetEmpMonthlyEnteredRecordResponse>> Load(string key, string yearMonth)
		{
			var result = await Mediator.Send(new GetEmpMonthlyEnteredRecordRequest()
			{
				Key = key,
				YearMonth = yearMonth
			});

			return result;
		}

		[HttpPost]
		public async Task<Result<SaveEmpMonthlyEnteredRecordResponse>> Save(SaveEmpMonthlyEnteredRecordRequest request)
		{
			if(request == null)
			{
				_logger.LogError(Errors.EmpMonthlyEnteredRecord.SaveRecordRequestModelNullError.Message);
				return Errors.EmpMonthlyEnteredRecord.SaveRecordRequestModelNullError;
			}

			var result = await Mediator.Send(request);

			return result;
		}

		[HttpPost("preview")]
		public async Task<Result<PreviewEmpMonthlySalaryResponse>> Preview(PreviewEmpMonthlySalaryRequest request)
		{
			var result = await Mediator.Send(request);

			return result;
		}

		[HttpPost("export")]
		public async Task<IActionResult> Export(ExportEmpMonthlySalaryRequest request)
		{
			var fileType = HttpContext.Request?.Headers["file-type"];
			var result = await Mediator.Send(request);

			if (result.Failure) return new ContentResult() { Content = JsonConvert.SerializeObject(result), 
				StatusCode = (int)HttpStatusCode.InternalServerError };

			var fileName = $"Monthly-employee-payroll-{request.YearMonth}.{fileType}";

			return File(result.Data.CsvSalaries, $"text/{fileType}", fileName);
		}

		[HttpGet("any")]
		public async Task<Result<AnyEmpMonthlyEnteredRecordResponse>> Any()
		{
			return await Mediator.Send(new AnyEmpMonthlyEnteredRecordRequest());
		}
	}
}
