using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculator.Infra.Persistent.Entities
{
	public class EncryptedEmpMonthlyEnteredRecord : EntityBase
	{
		public string EncryptedRecord { get; set; }
		public string EncryptedYearMonth { get; set; }
		public int StandardWorkingDays { get; set; }
		public EncryptedEmpMonthlySalary EncryptedEmpMonthlySalary { get; set; }
	}
}
