using MediatR;
using SalaryCalculator.Application.Models.ResponseModel;
using SalaryCalculator.Core.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculator.Application.Models.RequestModel
{
	public class ExportEmpMonthlySalaryRequest : IRequest<Result<ExportEmpMonthlySalaryResponse>>
	{
		public string YearMonth { get; set; }
		public string Key { get; set; }
	}
}
