namespace SalaryCalculator.Core.Domain
{
	public class NetIncome
	{
		public Money Amount { get; }

		public NetIncome(
			TotalMonthlyIncome totalMonthlyIncome,
			EmployeeHealthCareInsurance employeeHealthcareInsurance,
			EmployeeSocialInsurance employeeSocialInsurance,
			EmployeeUnemploymentInsurance employeeUnemploymentInsurance,
			EmployeeUnionFee employeeUnionFee,
			PersonalIncomeTax pit)
		{
			Amount = totalMonthlyIncome.Amount - 
			         employeeHealthcareInsurance.Amount - 
			         employeeSocialInsurance.Amount - 
			         employeeUnemploymentInsurance.Amount - 
			         employeeUnionFee.Amount - 
			         pit.Amount;
		}
	}
}