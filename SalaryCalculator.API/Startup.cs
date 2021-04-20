using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using SalaryCalculator.API.Common.Exceptions;
using SalaryCalculator.API.Common.Policies;
using SalaryCalculator.Application.Common.Registras;
using SalaryCalculator.Infra.Persistent.Common.Registras;

namespace SalaryCalculator.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			IdentityModelEventSource.ShowPII = true;

			services.AddControllers().AddFluentValidation();

			services.AddInfrastructure(Configuration);
			services.AddApplication(Configuration);
			var keyPath = Configuration.GetSection("DataProtectionConfiguration:KeyPath")
				.Get<string>();
			services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(keyPath));

			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy", p =>
				{
					p.AllowAnyHeader()
						.AllowAnyMethod()
						.AllowAnyOrigin();
				});
			});

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.Authority = Configuration["Auth:Authority"];
				options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateActor = false,
				};
			});
			services.AddAuthorization(options =>
			{
				options.AddPolicy("HRMSalaryPolicy", policy =>
				{
					policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
					policy.AddRequirements(new HRMSalaryRequirement("hrm_salary"));
				});
			});
			services.AddSingleton<IAuthorizationHandler, HRMSalaryRequirementHandler>();
			services.AddSwaggerGen();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMiddleware(typeof(GlobalExceptionMiddleware));

			app.UseCors("CorsPolicy");

			app.UseHttpsRedirection();

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});

		}
	}
}
