using MediatR;
using SalaryCalculator.Application.Models.ResponseModel;
using SalaryCalculator.Core.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculator.Application.Models.RequestModel
{
	public class PreviewEmpMonthlySalaryRequest : IRequest<Result<PreviewEmpMonthlySalaryResponse>>
	{
		public string Key { get; set; }
		public string YearMonth { get; set; }
	}
}
