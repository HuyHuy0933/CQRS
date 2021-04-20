using System;
using System.Collections.Generic;
using System.Text;
using SalaryCalculator.Infra.Persistent.Entities;

namespace SalaryCalculator.Infra.Persistent.Entities
{
    public class SalaryCurrency : EntityBase
    {
        public string Value { get; set; }

        public IList<SalarySetting> SalarySettings { get; set; }
        public IList<ProgressiveTaxRateSetting> ProgressiveTaxRateSettings { get; set; }
    }
}
