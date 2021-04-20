using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalaryCalculator.Infra.Persistent.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculator.Infra.Persistent.Configurations
{
	public class EmployeeProfileConfiguratio : IEntityTypeConfiguration<SyncEmployeeProfile>
	{
		public void Configure(EntityTypeBuilder<SyncEmployeeProfile> builder)
		{
			builder.Property(x => x.Id)
				.IsRequired();
			builder.Property(x => x.InUnion).HasDefaultValue(false);
			builder.Property(x => x.TerminatedDate).IsRequired(false);
		}
	}
}
