using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalaryCalculator.Infra.Persistent.Entities;

namespace SalaryCalculator.Infra.Persistent.Configurations
{
    public class SalaryCurrencyConfiguration : IEntityTypeConfiguration<SalaryCurrency>
    {
        public void Configure(EntityTypeBuilder<SalaryCurrency> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired();
        }
    }
}
