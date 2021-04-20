using MediatR;
using SalaryCalculator.Application.Models.RequestModel;
using SalaryCalculator.Application.Models.ResponseModel;
using SalaryCalculator.Core.Framework;
using SalaryCalculator.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SalaryCalculator.Core.Common.Extensions;

namespace SalaryCalculator.Application.CQRS.Queries.EmpMonthlySalary
{
	public class AnyEmpMonthlyEnteredRecordHandler : 
		IRequestHandler<AnyEmpMonthlyEnteredRecordRequest, Result<AnyEmpMonthlyEnteredRecordResponse>>
	{
		private readonly IEmployeeMonthlyEnteredRecordRepository _recordRepo;

		public AnyEmpMonthlyEnteredRecordHandler(
			IEmployeeMonthlyEnteredRecordRepository recordRepo)
		{
			_recordRepo = recordRepo;
		}
		public async Task<Result<AnyEmpMonthlyEnteredRecordResponse>> Handle(
			AnyEmpMonthlyEnteredRecordRequest request, 
			CancellationToken cancellationToken)
		{
			return (await _recordRepo.AnyAsync()).Map(x => new AnyEmpMonthlyEnteredRecordResponse { Any = x });
		}
	}
}
