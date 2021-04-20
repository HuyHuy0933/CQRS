namespace SalaryCalculator.Core.Domain
{
	public class TotalDeduction
	{
		public TotalDeduction(
			EmployeeMonthlyRecord employeeMonthlyRecord,
			SalaryConfig salaryConfig
		)
		{
			Amount = employeeMonthlyRecord.IsOnProbation()
				? Money.ZeroVND
				: salaryConfig.PersonalDeduction + 
				  (employeeMonthlyRecord.NumberOfDependants * salaryConfig.DependantDeduction);
		}

		public Money Amount { get; }
	}
}