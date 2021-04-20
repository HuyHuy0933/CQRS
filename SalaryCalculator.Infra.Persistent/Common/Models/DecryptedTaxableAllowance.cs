using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculator.Infra.Persistent.Common.Models
{
	public class DecryptedTaxableAllowance
	{
		public decimal Amount { get; set; }
		public string Name { get; set; }
	}
}
