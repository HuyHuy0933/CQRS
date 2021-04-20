using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalaryCalculator.Infra.Persistent.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculator.Infra.Persistent.Configurations
{
	public class EncryptedEmpMonthlySalaryConfiguration : IEntityTypeConfiguration<EncryptedEmpMonthlySalary>
	{
		public void Configure(EntityTypeBuilder<EncryptedEmpMonthlySalary> builder)
		{
			builder.Property(x => x.Id)
				.IsRequired();

			builder.Property(x => x.EncryptedSalary).HasColumnType("nvarchar(max)");

			builder.HasOne<EncryptedEmpMonthlyEnteredRecord>(x => x.EncryptedEmpMonthlyEnteredRecord)
				.WithOne(y => y.EncryptedEmpMonthlySalary)
				.HasForeignKey<EncryptedEmpMonthlySalary>(z => z.EncryptedRecordId);
		}
	}
}
