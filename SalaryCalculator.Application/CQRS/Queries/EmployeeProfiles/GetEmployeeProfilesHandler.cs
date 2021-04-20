using MediatR;
using SalaryCalculator.Application.Models.RequestModel;
using SalaryCalculator.Application.Models.ResponseModel;
using SalaryCalculator.Core.Framework;
using SalaryCalculator.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SalaryCalculator.Core.Common.Extensions;

namespace SalaryCalculator.Application.CQRS.Queries.EmployeeProfiles
{
	public class GetEmployeeProfilesHandler : IRequestHandler<GetEmployeeProfilesRequest, Result<GetEmployeeProfilesResponse>>
	{
		private readonly IEmployeeProfileRepository _profileRepo;

		public GetEmployeeProfilesHandler(
			IEmployeeProfileRepository profileRepo)
		{
			_profileRepo = profileRepo;
		}
		public async Task<Result<GetEmployeeProfilesResponse>> Handle(GetEmployeeProfilesRequest request, CancellationToken cancellationToken)
		{
			var result = await _profileRepo.GetAsync(request.YearMonth);
			return result.Map(x => new GetEmployeeProfilesResponse(
				result.Data.Select(x => new EmployeeProfilesResponse(
					email: x.Email,
					fullname: x.Fullname)).ToArray()));
		}
	}
}
