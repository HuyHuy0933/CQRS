using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalaryCalculator.Infra.Persistent.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculator.Infra.Persistent.Configurations
{
	public class EncryptedEmpMonthlyEnteredRecordConfiguration : IEntityTypeConfiguration<EncryptedEmpMonthlyEnteredRecord>
	{
		public void Configure(EntityTypeBuilder<EncryptedEmpMonthlyEnteredRecord> builder)
		{
			builder.Property(x => x.Id)
				.IsRequired();

			builder.Property(x => x.EncryptedRecord).HasColumnType("nvarchar(max)");
			builder.HasIndex(x => x.EncryptedYearMonth).IsUnique();
		}
	}
}
