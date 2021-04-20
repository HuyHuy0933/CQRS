using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SalaryCalculator.Infra.Persistent.Entities;

namespace SalaryCalculator.Infra.Persistent.Configurations
{
	public class SalarySettingConfiguration : IEntityTypeConfiguration<SalarySetting>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SalarySetting> builder)
		{
			builder.Property(x => x.Id)
				.IsRequired();

            builder.HasOne<SalaryCurrency>(x => x.SalaryCurrency)
                .WithMany(y => y.SalarySettings)
                .HasForeignKey(z => z.CurrencyId);
        }
	}
}