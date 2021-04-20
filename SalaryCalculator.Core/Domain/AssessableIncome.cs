namespace SalaryCalculator.Core.Domain
{
	public class AssessableIncome
	{
		public Money Amount { get; }

		public AssessableIncome(
			TaxableIncome taxableIncome,
			EmployeeSocialInsurance employeeSocialInsurance,
			EmployeeHealthCareInsurance employeeHealthCareInsurance,
			EmployeeUnemploymentInsurance employeeUnemploymentInsurance,
			EmployeeUnionFee employeeUnionFee,
			TotalDeduction totalDeduction)
		{
			var result = taxableIncome.Amount - 
			         employeeSocialInsurance.Amount - 
			         employeeHealthCareInsurance.Amount - 
			         employeeUnemploymentInsurance.Amount - 
			         employeeUnionFee.Amount - 
			         totalDeduction.Amount;

			Amount = result < Money.ZeroVND ? Money.ZeroVND : result;
		}
	}
}