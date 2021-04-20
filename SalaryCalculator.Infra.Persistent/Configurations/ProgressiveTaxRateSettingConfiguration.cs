using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalaryCalculator.Infra.Persistent.Entities;

namespace SalaryCalculator.Infra.Persistent.Configurations
{
    public class ProgressiveTaxRateSettingConfiguration : IEntityTypeConfiguration<ProgressiveTaxRateSetting>
    {
        public void Configure(EntityTypeBuilder<ProgressiveTaxRateSetting> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired();

            builder.HasOne<SalaryCurrency>(x => x.SalaryCurrency)
                .WithMany(y => y.ProgressiveTaxRateSettings)
                .HasForeignKey(z => z.CurrencyId);

            builder.Property(x => x.UpperBound).HasColumnType("decimal(34,2)");
        }
    }
}
