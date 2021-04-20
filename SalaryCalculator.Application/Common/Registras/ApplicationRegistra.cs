using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalaryCalculator.Application.Common.Helpers;
using SalaryCalculator.Application.Events.Consumers;

namespace SalaryCalculator.Application.Common.Registras
{
	public static class ApplicationRegistra
	{
		public static IServiceCollection AddApplication(this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			services.AddMediatR(Assembly.GetExecutingAssembly());
			services.AddScoped<IFileHelper, FileHelper>();

			services.AddMassTransit(s =>
			{
				s.AddConsumer<EmployeeProfileChangedConsumer>();
				s.AddConsumer<EmployeeProfileDeletedConsumer>();
				//s.SetKebabCaseEndpointNameFormatter();

				s.UsingRabbitMq((context, config) =>
				{
					config.Host(configuration["RabbitMq:Host"], h => 
					{
						h.Username(configuration["RabbitMq:Username"]);
						h.Password(configuration["RabbitMq:Password"]);
					});
					config.ClearMessageDeserializers();
					config.UseRawJsonSerializer();

					config.ReceiveEndpoint("employee-profile-changed", e =>
					{
						e.ConfigureConsumeTopology = false;
						e.ConfigureConsumer<EmployeeProfileChangedConsumer>(context);
					});

					config.ReceiveEndpoint("employee-profile-deleted", e =>
					{
						e.ConfigureConsumeTopology = false;
						e.ConfigureConsumer<EmployeeProfileDeletedConsumer>(context);
					});

					config.ConfigureEndpoints(context);
				});
				
			});

			services.AddMassTransitHostedService();
			return services;
		}
	}
}
