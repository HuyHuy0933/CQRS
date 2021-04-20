using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculator.Infra.Persistent.Entities
{
	public class EncryptedEmpMonthlySalary : EntityBase
	{
		public string EncryptedSalary { get; set; }

		public Guid EncryptedRecordId { get; set; }
		public bool IsLatest { get; set; }
		public EncryptedEmpMonthlyEnteredRecord EncryptedEmpMonthlyEnteredRecord { get; set; }
	}
}
