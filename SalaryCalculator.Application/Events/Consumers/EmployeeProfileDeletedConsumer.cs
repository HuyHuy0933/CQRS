using MassTransit;
using Microsoft.Extensions.Logging;
using SalaryCalculator.Application.Models.Event;
using SalaryCalculator.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SalaryCalculator.Application.Events.Consumers
{
	class EmployeeProfileDeletedConsumer : IConsumer<EmployeeProfileDeletedEvent>
	{
		private readonly ILogger<EmployeeProfileDeletedConsumer> _logger;
		private readonly IEmployeeProfileRepository _profileRepo;
		private readonly IEmployeeMonthlySalaryRepository _salaryRepo;
		public EmployeeProfileDeletedConsumer(
			ILogger<EmployeeProfileDeletedConsumer> logger,
			IEmployeeProfileRepository profileRepo,
			IEmployeeMonthlySalaryRepository salaryRepo)
		{
			_logger = logger;
			_profileRepo = profileRepo;
			_salaryRepo = salaryRepo;
		}
		public async Task Consume(ConsumeContext<EmployeeProfileDeletedEvent> context)
		{
			_logger.LogInformation("Start EmployeeProfileDeletedConsumer");
			var profileDeletedEvent = context.Message;
			var start = new DateTime(1970, 1, 1, 0, 0, 0, 0);

			var deletedProfile = await _profileRepo.DeleteAsync(profileDeletedEvent.Email, 
				start.AddMilliseconds(profileDeletedEvent.SendDate));
			if (deletedProfile.Failure) _logger.LogError(deletedProfile.Error.Message);

			var yearMonth = start.AddMilliseconds(profileDeletedEvent.SendDate).ToString("yyyy-MM");
			var mark = await _salaryRepo.MarkNotLatestAsync(yearMonth);
			if (mark.Failure) _logger.LogError(mark.Error.Message);
			_logger.LogInformation("End EmployeeProfileDeletedConsumer");
		}
	}
}
